﻿using NETworkManager.Utilities;
using NETworkManager.Models.Network;
using NETworkManager.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using NETworkManager.Controls;
using Dragablz;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NETworkManager.Models.Export;
using NETworkManager.Views;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace NETworkManager.ViewModels;

public class SNMPViewModel : ViewModelBase
{
    #region Variables
    private readonly IDialogCoordinator _dialogCoordinator;

    private CancellationTokenSource _cancellationTokenSource;

    private readonly int _tabId;

    private readonly bool _isLoading;

    private string _host;
    public string Host
    {
        get => _host;
        set
        {
            if (value == _host)
                return;

            _host = value;
            OnPropertyChanged();
        }
    }

    public ICollectionView HostHistoryView { get; }

    public IEnumerable<SNMPMode> Modes { get; set; }

    private SNMPMode _mode;
    public SNMPMode Mode
    {
        get => _mode;
        set
        {
            if (value == _mode)
                return;

            if (!_isLoading)
                SettingsManager.Current.SNMP_Mode = value;

            _mode = value;
            OnPropertyChanged();

            // Re-validate OID if mode changed
            OnPropertyChanged(nameof(Oid));
        }
    }

    public IEnumerable<SNMPVersion> Versions { get; }

    private SNMPVersion _version;
    public SNMPVersion Version
    {
        get => _version;
        set
        {
            if (value == _version)
                return;

            if (!_isLoading)
                SettingsManager.Current.SNMP_Version = value;

            _version = value;
            OnPropertyChanged();
        }
    }

    private string _oid;
    public string Oid
    {
        get => _oid;
        set
        {
            if (value == _oid)
                return;

            _oid = value;
            OnPropertyChanged();
        }
    }

    public ICollectionView OidHistoryView { get; }

    public IEnumerable<SNMPV3Security> Securities { get; }

    private SNMPV3Security _security;
    public SNMPV3Security Security
    {
        get => _security;
        set
        {
            if (value == _security)
                return;

            if (!_isLoading)
                SettingsManager.Current.SNMP_Security = value;

            _security = value;
            OnPropertyChanged();
        }
    }

    private bool _isCommunityEmpty = true; // Initial it's empty
    public bool IsCommunityEmpty
    {
        get => _isCommunityEmpty;
        set
        {
            if (value == _isCommunityEmpty)
                return;

            _isCommunityEmpty = value;
            OnPropertyChanged();
        }
    }

    private SecureString _community;
    public SecureString Community
    {
        get => _community;
        set
        {
            if (value == _community)
                return;

            // Validate the community string
            IsCommunityEmpty = value == null || string.IsNullOrEmpty(SecureStringHelper.ConvertToString(value));

            _community = value;
            OnPropertyChanged();
        }
    }

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if (value == _username)
                return;

            _username = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<SNMPV3AuthenticationProvider> AuthenticationProviders { get; }

    private SNMPV3AuthenticationProvider _authenticationProvider;
    public SNMPV3AuthenticationProvider AuthenticationProvider
    {
        get => _authenticationProvider;
        set
        {
            if (value == _authenticationProvider)
                return;

            if (!_isLoading)
                SettingsManager.Current.SNMP_AuthenticationProvider = value;

            _authenticationProvider = value;
            OnPropertyChanged();
        }
    }

    private bool _isAuthEmpty = true; // Initial it's empty
    public bool IsAuthEmpty
    {
        get => _isAuthEmpty;
        set
        {
            if (value == _isAuthEmpty)
                return;

            _isAuthEmpty = value;
            OnPropertyChanged();
        }
    }

    private SecureString _auth;
    public SecureString Auth
    {
        get => _auth;
        set
        {
            if (value == _auth)
                return;

            // Validate the auth string
            IsAuthEmpty = value == null || string.IsNullOrEmpty(SecureStringHelper.ConvertToString(value));

            _auth = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<SNMPV3PrivacyProvider> PrivacyProviders { get; }

    private SNMPV3PrivacyProvider _privacyProvider;
    public SNMPV3PrivacyProvider PrivacyProvider
    {
        get => _privacyProvider;
        set
        {
            if (value == _privacyProvider)
                return;

            if (!_isLoading)
                SettingsManager.Current.SNMP_PrivacyProvider = value;

            _privacyProvider = value;
            OnPropertyChanged();
        }
    }

    private bool _isPrivEmpty = true; // Initial it's empty
    public bool IsPrivEmpty
    {
        get => _isPrivEmpty;
        set
        {
            if (value == _isPrivEmpty)
                return;

            _isPrivEmpty = value;
            OnPropertyChanged();
        }
    }

    private SecureString _priv;
    public SecureString Priv
    {
        get => _priv;
        set
        {
            if (value == _priv)
                return;

            // Validate the auth string
            IsPrivEmpty = value == null || string.IsNullOrEmpty(SecureStringHelper.ConvertToString(value));

            _priv = value;
            OnPropertyChanged();
        }
    }

    private string _data = string.Empty;
    public string Data
    {
        get => _data;
        set
        {
            if (value == _data)
                return;

            _data = value;
            OnPropertyChanged();
        }
    }

    private bool _isRunning;
    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            if (value == _isRunning)
                return;

            _isRunning = value;
            OnPropertyChanged();
        }
    }

    private bool _cancelScan;
    public bool CancelScan
    {
        get => _cancelScan;
        set
        {
            if (value == _cancelScan)
                return;

            _cancelScan = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<SNMPInfo> _queryResults = new();
    public ObservableCollection<SNMPInfo> QueryResults
    {
        get => _queryResults;
        set
        {
            if (Equals(value, _queryResults))
                return;

            _queryResults = value;
        }
    }

    public ICollectionView ResultsView { get; }

    private SNMPInfo _selectedResult;
    public SNMPInfo SelectedResult
    {
        get => _selectedResult;
        set
        {
            if (value == _selectedResult)
                return;

            _selectedResult = value;
            OnPropertyChanged();
        }
    }

    private IList _selectedResults = new ArrayList();
    public IList SelectedResults
    {
        get => _selectedResults;
        set
        {
            if (Equals(value, _selectedResults))
                return;

            _selectedResults = value;
            OnPropertyChanged();
        }
    }


    private bool _isStatusMessageDisplayed;
    public bool IsStatusMessageDisplayed
    {
        get => _isStatusMessageDisplayed;
        set
        {
            if (value == _isStatusMessageDisplayed)
                return;

            _isStatusMessageDisplayed = value;
            OnPropertyChanged();
        }
    }

    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        private set
        {
            if (value == _statusMessage)
                return;

            _statusMessage = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Contructor, load settings
    public SNMPViewModel(IDialogCoordinator instance, int tabId, SNMPSessionInfo sessionInfo)
    {
        _isLoading = true;

        _dialogCoordinator = instance;

        _tabId = tabId;
        Host = sessionInfo?.Host;

        // Set collection view
        HostHistoryView = CollectionViewSource.GetDefaultView(SettingsManager.Current.SNMP_HostHistory);
        OidHistoryView = CollectionViewSource.GetDefaultView(SettingsManager.Current.SNMP_OidHistory);

        // Result view
        ResultsView = CollectionViewSource.GetDefaultView(QueryResults);
        ResultsView.SortDescriptions.Add(new SortDescription(nameof(SNMPInfo.Oid), ListSortDirection.Ascending));

        // OID
        Oid = sessionInfo?.OID;

        // Modes
        Modes = new List<SNMPMode> { SNMPMode.Get, SNMPMode.Walk, SNMPMode.Set };
        Mode = Modes.FirstOrDefault(x => x == sessionInfo.Mode);
        
        // Versions (v1, v2c, v3)
        Versions = Enum.GetValues(typeof(SNMPVersion)).Cast<SNMPVersion>().ToList();
        Version = Versions.FirstOrDefault(x => x == sessionInfo.Version);

        // Community
        if(Version != SNMPVersion.V3)
            Community = sessionInfo?.Community;

        // Security
        Securities = new List<SNMPV3Security> { SNMPV3Security.NoAuthNoPriv, SNMPV3Security.AuthNoPriv, SNMPV3Security.AuthPriv };
        Security = Securities.FirstOrDefault(x => x == sessionInfo.Security);

        // Username
        if(Version == SNMPVersion.V3)
            Username = sessionInfo?.Username;

        // Auth
        AuthenticationProviders = Enum.GetValues(typeof(SNMPV3AuthenticationProvider)).Cast<SNMPV3AuthenticationProvider>().ToList();
        AuthenticationProvider = AuthenticationProviders.FirstOrDefault(x => x == sessionInfo.AuthenticationProvider);
       
        if(Version == SNMPVersion.V3 && Security != SNMPV3Security.NoAuthNoPriv)
            Auth = sessionInfo?.Auth;

        // Priv
        PrivacyProviders = Enum.GetValues(typeof(SNMPV3PrivacyProvider)).Cast<SNMPV3PrivacyProvider>().ToList();
        PrivacyProvider = PrivacyProviders.FirstOrDefault(x => x == sessionInfo.PrivacyProvider);

        if (Version == SNMPVersion.V3 && Security == SNMPV3Security.AuthPriv)
            Priv = sessionInfo?.Priv;

        _isLoading = false;
    }
    #endregion

    #region ICommands & Actions
    public ICommand WorkCommand => new RelayCommand(p => WorkAction(), Work_CanExecute);

    private bool Work_CanExecute(object parameter) => Application.Current.MainWindow != null && !((MetroWindow)Application.Current.MainWindow).IsAnyDialogOpen;

    private void WorkAction()
    {
        Work();
    }

    public ICommand OpenMibProfilesCommand => new RelayCommand(_ => OpenMibProfilesAction());


    private void OpenMibProfilesAction()
    {
        OpenMibProfileSelection().ConfigureAwait(false);
    }

    public ICommand ExportCommand => new RelayCommand(_ => ExportAction());

    private async void ExportAction()
    {
        var customDialog = new CustomDialog
        {
            Title = Localization.Resources.Strings.Export
        };

        var exportViewModel = new ExportViewModel(async instance =>
        {
            await _dialogCoordinator.HideMetroDialogAsync(this, customDialog);

            try
            {
                ExportManager.Export(instance.FilePath, instance.FileType, instance.ExportAll ? QueryResults : new ObservableCollection<SNMPInfo>(SelectedResults.Cast<SNMPInfo>().ToArray()));
            }
            catch (Exception ex)
            {
                var settings = AppearanceManager.MetroDialog;
                settings.AffirmativeButtonText = Localization.Resources.Strings.OK;

                await _dialogCoordinator.ShowMessageAsync(this, Localization.Resources.Strings.Error, Localization.Resources.Strings.AnErrorOccurredWhileExportingTheData + Environment.NewLine + Environment.NewLine + ex.Message, MessageDialogStyle.Affirmative, settings);
            }

            SettingsManager.Current.SNMP_ExportFileType = instance.FileType;
            SettingsManager.Current.SNMP_ExportFilePath = instance.FilePath;
        }, _ =>
        {
            _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
        }, new[]
        {
            ExportFileType.Csv, ExportFileType.Xml, ExportFileType.Json
        }, true, SettingsManager.Current.SNMP_ExportFileType, SettingsManager.Current.SNMP_ExportFilePath);

        customDialog.Content = new ExportDialog
        {
            DataContext = exportViewModel
        };

        await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);
    }
    #endregion

    #region Methods
    private void Work()
    {
        if (IsRunning)
            StopWork();
        else
            StartWork();
    }

    private async void StartWork()
    {
        IsStatusMessageDisplayed = false;
        IsRunning = true;

        QueryResults.Clear();

        // Change the tab title (not nice, but it works)
        var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

        if (window != null)
        {
            foreach (var tabablzControl in VisualTreeHelper.FindVisualChildren<TabablzControl>(window))
            {
                tabablzControl.Items.OfType<DragablzTabItem>().First(x => x.Id == _tabId).Header = Host;
            }
        }

        // Try to parse the string into an IP-Address
        if (!IPAddress.TryParse(Host, out var ipAddress))
        {
            var dnsResult = await DNSClientHelper.ResolveAorAaaaAsync(Host, SettingsManager.Current.Network_ResolveHostnamePreferIPv4);

            if (dnsResult.HasError)
            {
                StatusMessage = DNSClientHelper.FormatDNSClientResultError(Host, dnsResult);
                IsStatusMessageDisplayed = true;
                IsRunning = false;
                return;
            }

            ipAddress = dnsResult.Value;
        }

        _cancellationTokenSource = new CancellationTokenSource();

        // SNMP...
        SNMPClient snmpClient = new();

        snmpClient.Received += Snmp_Received;
        snmpClient.DataUpdated += SnmpClient_DataUpdated;
        snmpClient.Error += Snmp_Error;
        snmpClient.Canceled += Snmp_Canceled;
        snmpClient.Complete += Snmp_Complete;

        var oidValue = Oid.Replace(" ", "");

        // Check if we have multiple OIDs for a Get request
        List<string> oids = new();

        if (Mode == SNMPMode.Get && oidValue.Contains(';'))
            oids.AddRange(oidValue.Split(';'));
        else
            oids.Add(oidValue);

        // SNMPv1 or v2c
        if (Version != SNMPVersion.V3)
        {
            var snmpOptions = new SNMPOptions(
                Community,
                Version,
                SettingsManager.Current.SNMP_Port,
                SettingsManager.Current.SNMP_WalkMode,
                _cancellationTokenSource.Token
            );

            switch (Mode)
            {
                case SNMPMode.Get:
                    snmpClient.GetAsync(ipAddress, oids, snmpOptions);
                    break;
                case SNMPMode.Walk:
                    snmpClient.WalkAsync(ipAddress, oids[0], snmpOptions);
                    break;
                case SNMPMode.Set:
                    snmpClient.SetAsync(ipAddress, oids[0], Data, snmpOptions);
                    break;
            }
        }
        // SNMPv3
        else
        {
            var snmpOptionsV3 = new SNMPOptionsV3(
                Security,
                Username,
                AuthenticationProvider,
                Auth,
                PrivacyProvider,
                Priv,
                SettingsManager.Current.SNMP_Port,
                SettingsManager.Current.SNMP_WalkMode,
                _cancellationTokenSource.Token
            );

            switch (Mode)
            {
                case SNMPMode.Get:
                    snmpClient.GetAsyncV3(ipAddress, oids, snmpOptionsV3);
                    break;
                case SNMPMode.Walk:
                    snmpClient.WalkAsyncV3(ipAddress, oids[0], snmpOptionsV3);
                    break;
                case SNMPMode.Set:
                    snmpClient.SetAsyncV3(ipAddress, oids[0], Data, snmpOptionsV3);
                    break;
            }
        }

        // Set timeout
        _cancellationTokenSource.CancelAfter(SettingsManager.Current.SNMP_Timeout);

        // Add to history...
        AddHostToHistory(Host);
        AddOidToHistory(Oid);
    }

    private void StopWork()
    {
        CancelScan = true;
        _cancellationTokenSource.Cancel();
    }

    public void OnClose()
    {

    }

    private async Task OpenMibProfileSelection()
    {
        var customDialog = new CustomDialog
        {
            Title = Localization.Resources.Strings.SelectOIDProfile
        };

        var viewModel = new SNMPOIDProfilesViewModel(async instance =>
        {
            await _dialogCoordinator.HideMetroDialogAsync(this, customDialog);

            Mode = instance.SelectedOIDProfile.Mode;
            Oid = instance.SelectedOIDProfile.OID;
        }, async _ =>
        {
            await _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
        });

        customDialog.Content = new SNMPOIDProfilesDialog
        {
            DataContext = viewModel
        };

        await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);
    }

    private void AddHostToHistory(string host)
    {
        // Create the new list
        var list = ListHelper.Modify(SettingsManager.Current.SNMP_HostHistory.ToList(), host, SettingsManager.Current.General_HistoryListEntries);

        // Clear the old items
        SettingsManager.Current.SNMP_HostHistory.Clear();
        OnPropertyChanged(nameof(Host)); // Raise property changed again, after the collection has been cleared

        // Fill with the new items
        list.ForEach(x => SettingsManager.Current.SNMP_HostHistory.Add(x));
    }

    private void AddOidToHistory(string oid)
    {
        // Create the new list
        var list = ListHelper.Modify(SettingsManager.Current.SNMP_OidHistory.ToList(), oid, SettingsManager.Current.General_HistoryListEntries);

        // Clear the old items
        SettingsManager.Current.SNMP_OidHistory.Clear();
        OnPropertyChanged(nameof(Oid)); // Raise property changed again, after the collection has been cleared

        // Fill with the new items
        list.ForEach(x => SettingsManager.Current.SNMP_OidHistory.Add(x));
    }
    #endregion

    #region Events
    private void Snmp_Received(object sender, SNMPReceivedArgs e)
    {
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
        {
            QueryResults.Add(e.Args);
        }));
    }

    private void SnmpClient_DataUpdated(object sender, EventArgs e)
    {
        StatusMessage = Localization.Resources.Strings.DataHasBeenUpdated;
        IsStatusMessageDisplayed = true;
    }

    private void Snmp_Error(object sender, EventArgs e)
    {
        StatusMessage = Mode == SNMPMode.Set ? Localization.Resources.Strings.ErrorInResponseCheckIfYouHaveWritePermissions : Localization.Resources.Strings.ErrorInResponse;
        IsStatusMessageDisplayed = true;
    }

    private void Snmp_Canceled(object sender, EventArgs e)
    {
        StatusMessage = CancelScan ? Localization.Resources.Strings.CanceledByUserMessage : Localization.Resources.Strings.TimeoutOnSNMPQuery;
        IsStatusMessageDisplayed = true;
    }

    private void Snmp_Complete(object sender, EventArgs e)
    {
        CancelScan = false;
        IsRunning = false;
    }
    #endregion
}

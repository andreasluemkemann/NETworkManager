---
layout: default
title: Next release
parent: Changelog
nav_order: 1
description: "Changelog for next release"
permalink: /Changelog/next-release
---

Version: **Next release** <br />
Release date: **xx.xx.2023**

**System requirements**

- Windows 10 / Server x64 (1809 or later)
- [.NET Desktop Runtime 6.x (LTS)](https://dotnet.microsoft.com/download/dotnet/6.0){:target="\_blank"}

Breaking Changes
{: .label .label-red }

New Feature
{: .label .label-green }

Experimental Feature
{: .label .label-purple }

## Breaking Changes

- Settings will be reset to default values due to refactoring [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Profiles and settings migration (indroduced in `2023.3.7.0`) from `%AppData%\NETworkManager` to `%UserProfile%\Documents\NETworkManager` removed. If you use a version before `2023.3.7.0` you have to install and start version `2023.6.27.1` before you install this version. [#2380](https://github.com/BornToBeRoot/NETworkManager/pull/2380){:target="\_blank"}
- Remove profile migration script to migrate from `2021.11.30.0` and before to a later version [#2388](https://github.com/BornToBeRoot/NETworkManager/pull/2388){:target="\_blank"}

## What's new?

- Dashboard
  - Check IP geolocation added [#2392](https://github.com/BornToBeRoot/NETworkManager/pull/2392){:target="\_blank"}
  - Check DNS resolver added [#2392](https://github.com/BornToBeRoot/NETworkManager/pull/2392){:target="\_blank"}
- Traceroute
  - Check IP geolocation for each public IP address added. Feature can be enabled/disabled in settings [#2477](https://github.com/BornToBeRoot/NETworkManager/pull/2477){:target="\_blank"}

## Experimental features

- Run command (HotKey: Ctrl+Shift+P) added. This feature allows you to open a command field to switch between applications and pass parameters to them.

{: .note}
Experimental features can be enabled in the settings under [`Settings > Update`](https://borntoberoot.net/NETworkManager/Documentation/Settings/Update#experimental-features){:target="\_blank"}.

## Improvements

- WiFi
  - MAC address renamed to BSSID [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
  - Values of a WiFi network can now be copied to clipboard via right click context menu [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- IP Scanner
  - Row can now be expanded / collapsed via button on the left side. Row is no longer expanded when selecting a row [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
  - Timestamp added to view and export (export format has changed for `csv`, `json` and `xml`) [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
  - Show status message if no reachable host was found [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Port Scanner
  - Show status message if no open port was found [#2483](
- Lookup - Ports
  - Search by port number and protocol (e.g. `22/tcp` or `161-162/udp`) added [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}

## Bugfixes

- Network Interface
  - Add missing scrollviewer in configure tab [#2410](https://github.com/BornToBeRoot/NETworkManager/pull/2410){:target="\_blank"}
- WiFi
  - Fix an issue where the UI was broken if a WiFi adapter was found that has no assiciated network interface (e.g. if the WiFi adapter is associated with a network bridge). The WiFi adapter is now ignored [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Traceroute
  - Copy time to clipboard for non reachable hops fixed [#2477](https://github.com/BornToBeRoot/NETworkManager/pull/2477){:target="\_blank"}
  - Export to Json fixed (if ip address of a hop cannot be resolved) [#2477](https://github.com/BornToBeRoot/NETworkManager/pull/2477){:target="\_blank"}
- Ping
  - Copy & Export time fixed (format is now `yyyy-MM-dd HH:mm:ss.fff`) [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- DNS Lookup
  - Fix a bug where the suffix is not appended [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- SNMP
  - Allow `.` as first character in OID (e.g. `1.3.6.1.2.1.1.1.0`) [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- SNTP Lookup
  - Copy & Export time fixed (format is now `yyyy-MM-dd HH:mm:ss.fff`) [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Whois
  - Allow domains with `.` at the end / improved domain validation [#2502](https://github.com/BornToBeRoot/NETworkManager/pull/2502){:target="\_blank"}
- Lookup - OUI
  - Don't show duplicate entries [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Lookup - Ports  
  - Don't show duplicate entries [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- Profiles
  - Prevent the application from crashing if a profile file cannot be loaded (profile management is then blocked) [#2464](https://github.com/BornToBeRoot/NETworkManager/pull/2464){:target="\_blank"}
  - Application name not translated [#2502](https://github.com/BornToBeRoot/NETworkManager/pull/2502){:target="\_blank"}
- Settings
  - Application name not translated [#2502](https://github.com/BornToBeRoot/NETworkManager/pull/2502){:target="\_blank"}
- Allow hostnames and domains with `.` at the end / improved validation for several tools [#2502](https://github.com/BornToBeRoot/NETworkManager/pull/2502){:target="\_blank"}
- Fix some focus issues when the enter key is pressed [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}
- PowerShell window is hidden again (e.g. configure network interface, add/remove arp entry, etc.) [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"}

## Other

- Code cleanup & Refactoring [#2388](https://github.com/BornToBeRoot/NETworkManager/pull/2388){:target="\_blank"} [#2392](https://github.com/BornToBeRoot/NETworkManager/pull/2392){:target="\_blank"} [#2477](https://github.com/BornToBeRoot/NETworkManager/pull/2477){:target="\_blank"} [#2483](https://github.com/BornToBeRoot/NETworkManager/pull/2483){:target="\_blank"} [#2502](https://github.com/BornToBeRoot/NETworkManager/pull/2502){:target="\_blank"}
- Language files updated [#transifex](https://github.com/BornToBeRoot/NETworkManager/pulls?q=author%3Aapp%2Ftransifex-integration){:target="\_blank"}
- Dependencies updated [#dependencies](https://github.com/BornToBeRoot/NETworkManager/pulls?q=author%3Aapp%2Fdependabot){:target="\_blank"}

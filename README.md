﻿# SE StA API

Eine REST-Schnittstelle, die für die Verwendunbg in der Studienarbeit Softwareengineering II konzipiert ist. Dazu eine Datenbank und ein Angular Frontend.

## Die Datenbank

PostreSQL Datenbank in einem Docker Container. Daten-Volume im Unterordner db.

## Die Schnittstelle

Die REST-Schnittstelle umfasst eine Swagger-UI, diese beinhaltet eine Übersicht über alle verfügbaren Endpunkte. Das ganze läuft ebenfalls in einem Docker-Container.

## Das Frontend

Angular Anwendung in einem Docker Container.
Kann <b>bei lokal installierter Angular CLI</b> im Unterverzeichnis SE_StA_Angular mit
`ng serve --open`
gestartet werden. (min. Angular CLI 16.0.3)

## Start der Anwendung

In diesem Verzeichnis ein Terminal öffnen und folgenden Befehl absetzen:
`docker-compose up`

# Datenmodell

Die funktionalen Anforderungen gehen aus dem Pflichtenheft hervor. Hier ist das vollständige Datenmodell inklusive Umsetzung der Berechtigungsstruktur gezeigt.

## Klassendiagramm

![Klassendiagramm](Classes.png)
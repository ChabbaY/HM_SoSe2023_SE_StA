-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Erstellungszeit: 10. Jul 2023 um 13:03
-- Server-Version: 8.0.33
-- PHP-Version: 8.1.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `api`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Addresses`
--

CREATE TABLE `Addresses` (
  `AddressId` int NOT NULL,
  `Street` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HouseNumber` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PostalCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Town` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AddressAddition` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CountryId` int NOT NULL,
  `TimeZoneId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Addresses`
--

INSERT INTO `Addresses` (`AddressId`, `Street`, `HouseNumber`, `PostalCode`, `Town`, `AddressAddition`, `CountryId`, `TimeZoneId`) VALUES
(1, 'Lothstraße', '34', '80335', 'München', '', 1, 1),
(2, 'Lothstraße', '64', '80335', 'München', '', 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `AspNetRoles`
--

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('43fb8ecd-49b7-44a7-b118-e10ac541dde3', 'Admin', 'ADMIN', NULL),
('6ed5a46c-e0ac-4f96-b32a-16cdbe4ecf7a', 'User', 'USER', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `AspNetUserRoles`
--

INSERT INTO `AspNetUserRoles` (`UserId`, `RoleId`) VALUES
('e734f4a7-7112-48ca-92ea-128196bcd697', '43fb8ecd-49b7-44a7-b118-e10ac541dde3'),
('e734f4a7-7112-48ca-92ea-128196bcd697', '6ed5a46c-e0ac-4f96-b32a-16cdbe4ecf7a');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('e734f4a7-7112-48ca-92ea-128196bcd697', 'se-sta', 'SE-STA', 'se.sta@chabbay.de', 'SE.STA@CHABBAY.DE', 1, 'AQAAAAIAAYagAAAAEKqoiwnLCsVw8GRDN7Rz29UMuKVOyEf/EoZPqXLMOahdNLFpWUfVywf4Jb33PqXDUQ==', 'STKVJ7VHAWYREYUPQMX7P4P5JXLRSQ4Z', 'a42e2208-9a99-4fd3-97e8-c2989bdaebca', NULL, 0, 0, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `BankAccounts`
--

CREATE TABLE `BankAccounts` (
  `BankAccountId` int NOT NULL,
  `Iban` varchar(22) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PaymentMethodId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `BankAccounts`
--

INSERT INTO `BankAccounts` (`BankAccountId`, `Iban`, `PaymentMethodId`) VALUES
(1, 'DE42472028562745230523', 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `BookingPositionRooms`
--

CREATE TABLE `BookingPositionRooms` (
  `BookingPositionRoomId` int NOT NULL,
  `Date` datetime(6) NOT NULL,
  `Price` double NOT NULL,
  `RoomId` int NOT NULL,
  `BookingId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `BookingPositionRooms`
--

INSERT INTO `BookingPositionRooms` (`BookingPositionRoomId`, `Date`, `Price`, `RoomId`, `BookingId`) VALUES
(1, '2011-07-20 23:00:00.000000', 100, 1, 1),
(2, '2012-07-20 23:00:00.000000', 160, 2, 1),
(3, '2013-07-20 23:00:00.000000', 160, 3, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `BookingPositionServices`
--

CREATE TABLE `BookingPositionServices` (
  `BookingPositionServiceId` int NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  `Price` double NOT NULL,
  `ServiceId` int NOT NULL,
  `BookingId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `BookingPositionServices`
--

INSERT INTO `BookingPositionServices` (`BookingPositionServiceId`, `DateTime`, `Price`, `ServiceId`, `BookingId`) VALUES
(1, '2023-07-12 12:27:27.000000', 2000, 3, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Bookings`
--

CREATE TABLE `Bookings` (
  `BookingId` int NOT NULL,
  `Number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Date` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TotalPrice` double NOT NULL,
  `CustomerId` int NOT NULL,
  `InvoiceId` int NOT NULL,
  `PaymentMethodId` int NOT NULL,
  `StatusId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Bookings`
--

INSERT INTO `Bookings` (`BookingId`, `Number`, `Date`, `TotalPrice`, `CustomerId`, `InvoiceId`, `PaymentMethodId`, `StatusId`) VALUES
(1, '1', '10.07.2023', 2420, 1, 1, 2, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Cashes`
--

CREATE TABLE `Cashes` (
  `CashId` int NOT NULL,
  `PaymentMethod` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Cashes`
--

INSERT INTO `Cashes` (`CashId`, `PaymentMethod`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ContactAddresses`
--

CREATE TABLE `ContactAddresses` (
  `ContactAddressId` int NOT NULL,
  `ContactId` int NOT NULL,
  `AddressId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `ContactAddresses`
--

INSERT INTO `ContactAddresses` (`ContactAddressId`, `ContactId`, `AddressId`) VALUES
(1, 1, 1),
(2, 2, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Contacts`
--

CREATE TABLE `Contacts` (
  `ContactId` int NOT NULL,
  `Salutation` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Contacts`
--

INSERT INTO `Contacts` (`ContactId`, `Salutation`, `Phone`) VALUES
(1, 'Hotel', '+49 89 1265-0'),
(2, 'Mr.', '');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Countries`
--

CREATE TABLE `Countries` (
  `CountryId` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Language` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Iso2` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Countries`
--

INSERT INTO `Countries` (`CountryId`, `Name`, `Language`, `Iso2`) VALUES
(1, 'Deutschland', 'Deutsch', 'DE');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `CountryTimeZones`
--

CREATE TABLE `CountryTimeZones` (
  `CountryTimeZoneId` int NOT NULL,
  `CountryId` int NOT NULL,
  `TimeZoneId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `CountryTimeZones`
--

INSERT INTO `CountryTimeZones` (`CountryTimeZoneId`, `CountryId`, `TimeZoneId`) VALUES
(1, 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `CreditCards`
--

CREATE TABLE `CreditCards` (
  `CreditCardId` int NOT NULL,
  `CardNumber` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PaymentMethod` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `CreditCards`
--

INSERT INTO `CreditCards` (`CreditCardId`, `CardNumber`, `PaymentMethod`) VALUES
(1, '12345678123456781234', 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Customers`
--

CREATE TABLE `Customers` (
  `CustomerId` int NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DateOfBirth` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContactId` int NOT NULL,
  `UserId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Customers`
--

INSERT INTO `Customers` (`CustomerId`, `FirstName`, `LastName`, `DateOfBirth`, `Number`, `ContactId`, `UserId`) VALUES
(1, 'Max', 'Mustermann', '01.01.1970', '1', 2, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Flights`
--

CREATE TABLE `Flights` (
  `FlightId` int NOT NULL,
  `FlightNumber` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Destination` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Flights`
--

INSERT INTO `Flights` (`FlightId`, `FlightNumber`, `Destination`, `ServiceId`) VALUES
(1, 'MH370', 'Valhalla', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Hotels`
--

CREATE TABLE `Hotels` (
  `HotelId` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Stars` int NOT NULL,
  `ContactId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Hotels`
--

INSERT INTO `Hotels` (`HotelId`, `Name`, `Stars`, `ContactId`) VALUES
(1, 'Hochschule München', 5, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Invoices`
--

CREATE TABLE `Invoices` (
  `InvoiceId` int NOT NULL,
  `Number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Invoices`
--

INSERT INTO `Invoices` (`InvoiceId`, `Number`) VALUES
(1, '1');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `PaymentMethods`
--

CREATE TABLE `PaymentMethods` (
  `PaymentMethodId` int NOT NULL,
  `CustomerId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `PaymentMethods`
--

INSERT INTO `PaymentMethods` (`PaymentMethodId`, `CustomerId`) VALUES
(1, 1),
(2, 1),
(3, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `RentalCars`
--

CREATE TABLE `RentalCars` (
  `RentalCarId` int NOT NULL,
  `CarModel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Seats` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `RentalCars`
--

INSERT INTO `RentalCars` (`RentalCarId`, `CarModel`, `Seats`, `ServiceId`) VALUES
(1, 'BMW3', '5', 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Rooms`
--

CREATE TABLE `Rooms` (
  `RoomId` int NOT NULL,
  `RoomNumber` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HotelId` int NOT NULL,
  `RoomTypeId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Rooms`
--

INSERT INTO `Rooms` (`RoomId`, `RoomNumber`, `HotelId`, `RoomTypeId`) VALUES
(1, '1', 1, 1),
(2, '2', 1, 2),
(3, '2B', 1, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `RoomTypes`
--

CREATE TABLE `RoomTypes` (
  `RoomTypeId` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefaultPrice` double NOT NULL,
  `PersonsCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `RoomTypes`
--

INSERT INTO `RoomTypes` (`RoomTypeId`, `Name`, `DefaultPrice`, `PersonsCount`) VALUES
(1, 'Single-Bed Room', 100, 1),
(2, 'Double-Bed Room', 160, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Services`
--

CREATE TABLE `Services` (
  `ServiceId` int NOT NULL,
  `ServiceTypeId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Services`
--

INSERT INTO `Services` (`ServiceId`, `ServiceTypeId`) VALUES
(1, 2),
(2, 2),
(3, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ServiceTypes`
--

CREATE TABLE `ServiceTypes` (
  `ServiceTypeId` int NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefaultPrice` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `ServiceTypes`
--

INSERT INTO `ServiceTypes` (`ServiceTypeId`, `Name`, `DefaultPrice`) VALUES
(1, 'Premium', 2000),
(2, 'Default', 1700);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Statuses`
--

CREATE TABLE `Statuses` (
  `StatusId` int NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Statuses`
--

INSERT INTO `Statuses` (`StatusId`, `Name`) VALUES
(1, 'Booked'),
(2, 'Reserved'),
(3, 'Cancelled');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `TimeZones`
--

CREATE TABLE `TimeZones` (
  `TimeZoneId` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `difUtc` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `TimeZones`
--

INSERT INTO `TimeZones` (`TimeZoneId`, `Name`, `difUtc`) VALUES
(1, 'CEST', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Wellnesses`
--

CREATE TABLE `Wellnesses` (
  `WellnessId` int NOT NULL,
  `Duration` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Wellnesses`
--

INSERT INTO `Wellnesses` (`WellnessId`, `Duration`, `Name`, `ServiceId`) VALUES
(1, '5', 'Sauna', 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20230621152909_migration-v0', '7.0.7'),
('20230625114457_migration-v1', '7.0.7'),
('20230701165619_migration-v2', '7.0.7');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `Addresses`
--
ALTER TABLE `Addresses`
  ADD PRIMARY KEY (`AddressId`);

--
-- Indizes für die Tabelle `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indizes für die Tabelle `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indizes für die Tabelle `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indizes für die Tabelle `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indizes für die Tabelle `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indizes für die Tabelle `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indizes für die Tabelle `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indizes für die Tabelle `BankAccounts`
--
ALTER TABLE `BankAccounts`
  ADD PRIMARY KEY (`BankAccountId`);

--
-- Indizes für die Tabelle `BookingPositionRooms`
--
ALTER TABLE `BookingPositionRooms`
  ADD PRIMARY KEY (`BookingPositionRoomId`);

--
-- Indizes für die Tabelle `BookingPositionServices`
--
ALTER TABLE `BookingPositionServices`
  ADD PRIMARY KEY (`BookingPositionServiceId`);

--
-- Indizes für die Tabelle `Bookings`
--
ALTER TABLE `Bookings`
  ADD PRIMARY KEY (`BookingId`);

--
-- Indizes für die Tabelle `Cashes`
--
ALTER TABLE `Cashes`
  ADD PRIMARY KEY (`CashId`);

--
-- Indizes für die Tabelle `ContactAddresses`
--
ALTER TABLE `ContactAddresses`
  ADD PRIMARY KEY (`ContactAddressId`);

--
-- Indizes für die Tabelle `Contacts`
--
ALTER TABLE `Contacts`
  ADD PRIMARY KEY (`ContactId`);

--
-- Indizes für die Tabelle `Countries`
--
ALTER TABLE `Countries`
  ADD PRIMARY KEY (`CountryId`);

--
-- Indizes für die Tabelle `CountryTimeZones`
--
ALTER TABLE `CountryTimeZones`
  ADD PRIMARY KEY (`CountryTimeZoneId`);

--
-- Indizes für die Tabelle `CreditCards`
--
ALTER TABLE `CreditCards`
  ADD PRIMARY KEY (`CreditCardId`);

--
-- Indizes für die Tabelle `Customers`
--
ALTER TABLE `Customers`
  ADD PRIMARY KEY (`CustomerId`);

--
-- Indizes für die Tabelle `Flights`
--
ALTER TABLE `Flights`
  ADD PRIMARY KEY (`FlightId`);

--
-- Indizes für die Tabelle `Hotels`
--
ALTER TABLE `Hotels`
  ADD PRIMARY KEY (`HotelId`);

--
-- Indizes für die Tabelle `Invoices`
--
ALTER TABLE `Invoices`
  ADD PRIMARY KEY (`InvoiceId`);

--
-- Indizes für die Tabelle `PaymentMethods`
--
ALTER TABLE `PaymentMethods`
  ADD PRIMARY KEY (`PaymentMethodId`);

--
-- Indizes für die Tabelle `RentalCars`
--
ALTER TABLE `RentalCars`
  ADD PRIMARY KEY (`RentalCarId`);

--
-- Indizes für die Tabelle `Rooms`
--
ALTER TABLE `Rooms`
  ADD PRIMARY KEY (`RoomId`);

--
-- Indizes für die Tabelle `RoomTypes`
--
ALTER TABLE `RoomTypes`
  ADD PRIMARY KEY (`RoomTypeId`);

--
-- Indizes für die Tabelle `Services`
--
ALTER TABLE `Services`
  ADD PRIMARY KEY (`ServiceId`);

--
-- Indizes für die Tabelle `ServiceTypes`
--
ALTER TABLE `ServiceTypes`
  ADD PRIMARY KEY (`ServiceTypeId`);

--
-- Indizes für die Tabelle `Statuses`
--
ALTER TABLE `Statuses`
  ADD PRIMARY KEY (`StatusId`);

--
-- Indizes für die Tabelle `TimeZones`
--
ALTER TABLE `TimeZones`
  ADD PRIMARY KEY (`TimeZoneId`);

--
-- Indizes für die Tabelle `Wellnesses`
--
ALTER TABLE `Wellnesses`
  ADD PRIMARY KEY (`WellnessId`);

--
-- Indizes für die Tabelle `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `Addresses`
--
ALTER TABLE `Addresses`
  MODIFY `AddressId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `BankAccounts`
--
ALTER TABLE `BankAccounts`
  MODIFY `BankAccountId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `BookingPositionRooms`
--
ALTER TABLE `BookingPositionRooms`
  MODIFY `BookingPositionRoomId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `BookingPositionServices`
--
ALTER TABLE `BookingPositionServices`
  MODIFY `BookingPositionServiceId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Bookings`
--
ALTER TABLE `Bookings`
  MODIFY `BookingId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Cashes`
--
ALTER TABLE `Cashes`
  MODIFY `CashId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `ContactAddresses`
--
ALTER TABLE `ContactAddresses`
  MODIFY `ContactAddressId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `Contacts`
--
ALTER TABLE `Contacts`
  MODIFY `ContactId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `Countries`
--
ALTER TABLE `Countries`
  MODIFY `CountryId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `CountryTimeZones`
--
ALTER TABLE `CountryTimeZones`
  MODIFY `CountryTimeZoneId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `CreditCards`
--
ALTER TABLE `CreditCards`
  MODIFY `CreditCardId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Customers`
--
ALTER TABLE `Customers`
  MODIFY `CustomerId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Flights`
--
ALTER TABLE `Flights`
  MODIFY `FlightId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Hotels`
--
ALTER TABLE `Hotels`
  MODIFY `HotelId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Invoices`
--
ALTER TABLE `Invoices`
  MODIFY `InvoiceId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `PaymentMethods`
--
ALTER TABLE `PaymentMethods`
  MODIFY `PaymentMethodId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `RentalCars`
--
ALTER TABLE `RentalCars`
  MODIFY `RentalCarId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Rooms`
--
ALTER TABLE `Rooms`
  MODIFY `RoomId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `RoomTypes`
--
ALTER TABLE `RoomTypes`
  MODIFY `RoomTypeId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `Services`
--
ALTER TABLE `Services`
  MODIFY `ServiceId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `ServiceTypes`
--
ALTER TABLE `ServiceTypes`
  MODIFY `ServiceTypeId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `Statuses`
--
ALTER TABLE `Statuses`
  MODIFY `StatusId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `TimeZones`
--
ALTER TABLE `TimeZones`
  MODIFY `TimeZoneId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `Wellnesses`
--
ALTER TABLE `Wellnesses`
  MODIFY `WellnessId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

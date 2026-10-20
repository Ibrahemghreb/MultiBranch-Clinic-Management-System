/*
================================================================================
 ClinicDB — Final Schema (v1.0)
 Target: Egypt & Gulf dental/clinic management (commercial desktop)
================================================================================
 BEFORE RUNNING:
 1) Backup existing ClinicDB if it contains production data.
 2) This script DROPS and recreates the database (development/bootstrap only).
    For production upgrades use a separate migration script (do not DROP).
 3) After deploy, set the admin password from the application (BCrypt).
    The seed user is created with MustChangePassword = 1 and a placeholder hash.
================================================================================
*/

USE master;
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'ClinicDB')
BEGIN
    ALTER DATABASE ClinicDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ClinicDB;
END
GO

-- Use instance default paths (avoid hard-coded MSSQL16 folder)
CREATE DATABASE ClinicDB
COLLATE Arabic_CI_AS;
GO

USE ClinicDB;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- =============================================
-- 1) System: branches, RBAC, users
-- =============================================

CREATE TABLE dbo.Branches (
    BranchID        INT IDENTITY(1,1) NOT NULL,
    BranchCode      NVARCHAR(20)  NULL,
    BranchName      NVARCHAR(100) NOT NULL,
    Address         NVARCHAR(200) NULL,
    Phone           NVARCHAR(20)  NULL,
    Email           NVARCHAR(100) NULL,
    OpenTime        TIME(0)       NULL,
    CloseTime       TIME(0)       NULL,
    TimeZoneId      NVARCHAR(50)  NOT NULL CONSTRAINT DF_Branches_TimeZoneId DEFAULT N'Africa/Cairo',
    IsActive        BIT           NOT NULL CONSTRAINT DF_Branches_IsActive DEFAULT (1),
    CreatedAt       DATETIME2(0)  NOT NULL CONSTRAINT DF_Branches_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy       INT           NULL,
    UpdatedAt       DATETIME2(0)  NULL,
    UpdatedBy       INT           NULL,
    CONSTRAINT PK_Branches PRIMARY KEY CLUSTERED (BranchID),
    CONSTRAINT CK_Branches_Hours CHECK (OpenTime IS NULL OR CloseTime IS NULL OR OpenTime < CloseTime)
);
GO

CREATE TABLE dbo.Roles (
    RoleID          INT IDENTITY(1,1) NOT NULL,
    RoleCode        NVARCHAR(30)  NOT NULL,
    RoleName        NVARCHAR(50)  NOT NULL,
    Description     NVARCHAR(200) NULL,
    IsSystem        BIT           NOT NULL CONSTRAINT DF_Roles_IsSystem DEFAULT (1),
    IsActive        BIT           NOT NULL CONSTRAINT DF_Roles_IsActive DEFAULT (1),
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (RoleID),
    CONSTRAINT UQ_Roles_RoleCode UNIQUE (RoleCode),
    CONSTRAINT UQ_Roles_RoleName UNIQUE (RoleName)
);
GO

CREATE TABLE dbo.Permissions (
    PermissionID    INT IDENTITY(1,1) NOT NULL,
    PermissionCode  NVARCHAR(50)  NOT NULL,
    PermissionName  NVARCHAR(100) NOT NULL,
    ModuleName      NVARCHAR(50)  NULL,
    Description     NVARCHAR(200) NULL,
    CONSTRAINT PK_Permissions PRIMARY KEY CLUSTERED (PermissionID),
    CONSTRAINT UQ_Permissions_Code UNIQUE (PermissionCode)
);
GO

CREATE TABLE dbo.RolePermissions (
    RoleID          INT NOT NULL,
    PermissionID    INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleID, PermissionID),
    CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID),
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionID) REFERENCES dbo.Permissions(PermissionID)
);
GO

CREATE TABLE dbo.Users (
    UserID                  INT IDENTITY(1,1) NOT NULL,
    Username                NVARCHAR(50)  NOT NULL,
    PasswordHash            NVARCHAR(255) NOT NULL,
    RoleID                  INT           NOT NULL,
    FullName                NVARCHAR(100) NULL,
    Phone                   NVARCHAR(20)  NULL,
    Email                   NVARCHAR(100) NULL,
    IsActive                BIT           NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT (1),
    IsLocked                BIT           NOT NULL CONSTRAINT DF_Users_IsLocked DEFAULT (0),
    FailedLoginAttempts     INT           NOT NULL CONSTRAINT DF_Users_FailedLoginAttempts DEFAULT (0),
    LockoutUntil            DATETIME2(0)  NULL,
    MustChangePassword      BIT           NOT NULL CONSTRAINT DF_Users_MustChangePassword DEFAULT (1),
    PasswordChangedAt       DATETIME2(0)  NULL,
    LastLoginAt             DATETIME2(0)  NULL,
    CreatedAt               DATETIME2(0)  NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy               INT           NULL,
    UpdatedAt               DATETIME2(0)  NULL,
    UpdatedBy               INT           NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (UserID),
    CONSTRAINT UQ_Users_Username UNIQUE (Username),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID),
    CONSTRAINT CK_Users_Email CHECK (Email IS NULL OR Email LIKE '%_@_%.__%')
);
GO

-- Reception / accountant scoped to branches (required for multi-branch clinics)
CREATE TABLE dbo.UserBranches (
    UserID          INT NOT NULL,
    BranchID        INT NOT NULL,
    IsDefault       BIT NOT NULL CONSTRAINT DF_UserBranches_IsDefault DEFAULT (0),
    CONSTRAINT PK_UserBranches PRIMARY KEY (UserID, BranchID),
    CONSTRAINT FK_UserBranches_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT FK_UserBranches_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID)
);
GO

-- Deferred self-FKs for audit columns
ALTER TABLE dbo.Branches ADD CONSTRAINT FK_Branches_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID);
ALTER TABLE dbo.Branches ADD CONSTRAINT FK_Branches_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES dbo.Users(UserID);
ALTER TABLE dbo.Users  ADD CONSTRAINT FK_Users_CreatedBy  FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID);
ALTER TABLE dbo.Users  ADD CONSTRAINT FK_Users_UpdatedBy  FOREIGN KEY (UpdatedBy) REFERENCES dbo.Users(UserID);
GO

-- =============================================
-- 2) Doctors & schedules
-- =============================================

CREATE TABLE dbo.Specialties (
    SpecialtyID     INT IDENTITY(1,1) NOT NULL,
    SpecialtyName   NVARCHAR(100) NOT NULL,
    IsActive        BIT NOT NULL CONSTRAINT DF_Specialties_IsActive DEFAULT (1),
    CONSTRAINT PK_Specialties PRIMARY KEY CLUSTERED (SpecialtyID),
    CONSTRAINT UQ_Specialties_Name UNIQUE (SpecialtyName)
);
GO

CREATE TABLE dbo.Doctors (
    DoctorID        INT IDENTITY(1,1) NOT NULL,
    UserID          INT NOT NULL,
    FirstName       NVARCHAR(50) NOT NULL,
    LastName        NVARCHAR(50) NOT NULL,
    Phone           NVARCHAR(20)  NULL,
    Email           NVARCHAR(100) NULL,
    LicenseNo       NVARCHAR(50)  NULL,
    IsActive        BIT NOT NULL CONSTRAINT DF_Doctors_IsActive DEFAULT (1),
    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_Doctors_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy       INT NULL,
    CONSTRAINT PK_Doctors PRIMARY KEY CLUSTERED (DoctorID),
    CONSTRAINT UQ_Doctors_UserID UNIQUE (UserID),
    CONSTRAINT FK_Doctors_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_Doctors_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.DoctorSpecialties (
    DoctorID        INT NOT NULL,
    SpecialtyID     INT NOT NULL,
    CONSTRAINT PK_DoctorSpecialties PRIMARY KEY (DoctorID, SpecialtyID),
    CONSTRAINT FK_DoctorSpecialties_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID) ON DELETE CASCADE,
    CONSTRAINT FK_DoctorSpecialties_Specialties FOREIGN KEY (SpecialtyID) REFERENCES dbo.Specialties(SpecialtyID)
);
GO

CREATE TABLE dbo.DoctorBranches (
    DoctorID        INT NOT NULL,
    BranchID        INT NOT NULL,
    IsPrimary       BIT NOT NULL CONSTRAINT DF_DoctorBranches_IsPrimary DEFAULT (0),
    CONSTRAINT PK_DoctorBranches PRIMARY KEY (DoctorID, BranchID),
    CONSTRAINT FK_DoctorBranches_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID) ON DELETE CASCADE,
    CONSTRAINT FK_DoctorBranches_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID)
);
GO

-- Weekly schedule + slot length (prevents manual double-booking when app enforces slots)
CREATE TABLE dbo.DoctorSchedules (
    ScheduleID      INT IDENTITY(1,1) NOT NULL,
    DoctorID        INT NOT NULL,
    BranchID        INT NOT NULL,
    DayOfWeek       TINYINT NOT NULL, -- 1=Sunday .. 7=Saturday (SET DATEFIRST 7 in app for Egypt)
    StartTime       TIME(0) NOT NULL,
    EndTime         TIME(0) NOT NULL,
    SlotMinutes     INT NOT NULL CONSTRAINT DF_DoctorSchedules_SlotMinutes DEFAULT (15),
    IsActive        BIT NOT NULL CONSTRAINT DF_DoctorSchedules_IsActive DEFAULT (1),
    CONSTRAINT PK_DoctorSchedules PRIMARY KEY CLUSTERED (ScheduleID),
    CONSTRAINT FK_DoctorSchedules_DoctorBranches FOREIGN KEY (DoctorID, BranchID) REFERENCES dbo.DoctorBranches(DoctorID, BranchID),
    CONSTRAINT CK_DoctorSchedules_Day CHECK (DayOfWeek BETWEEN 1 AND 7),
    CONSTRAINT CK_DoctorSchedules_Time CHECK (StartTime < EndTime),
    CONSTRAINT CK_DoctorSchedules_Slot CHECK (SlotMinutes BETWEEN 5 AND 240)
);
GO

-- =============================================
-- 3) Patients & clinical records
-- =============================================

CREATE TABLE dbo.Patients (
    PatientID           INT IDENTITY(1,1) NOT NULL,
    PatientCode         NVARCHAR(30)  NULL,
    FirstName           NVARCHAR(50)  NOT NULL,
    LastName            NVARCHAR(50)  NOT NULL,
    DateOfBirth         DATE          NULL,
    Gender              NVARCHAR(10)  NULL,
    NationalID          NVARCHAR(20)  NULL,
    PassportNo          NVARCHAR(30)  NULL,
    Phone               NVARCHAR(20)  NULL,
    Phone2              NVARCHAR(20)  NULL,
    Email               NVARCHAR(100) NULL,
    Address             NVARCHAR(200) NULL,
    City                NVARCHAR(50)  NULL,
    CountryCode         CHAR(2)       NOT NULL CONSTRAINT DF_Patients_CountryCode DEFAULT ('EG'),
    BloodType           NVARCHAR(5)   NULL,
    EmergencyContact    NVARCHAR(50)  NULL,
    EmergencyPhone      NVARCHAR(20)  NULL,
    Notes               NVARCHAR(500) NULL,
    IsDeleted           BIT NOT NULL CONSTRAINT DF_Patients_IsDeleted DEFAULT (0),
    DeletedAt           DATETIME2(0)  NULL,
    DeletedBy           INT           NULL,
    CreatedAt           DATETIME2(0)  NOT NULL CONSTRAINT DF_Patients_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy           INT           NULL,
    UpdatedAt           DATETIME2(0)  NULL,
    UpdatedBy           INT           NULL,
    CONSTRAINT PK_Patients PRIMARY KEY CLUSTERED (PatientID),
    CONSTRAINT FK_Patients_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_Patients_DeletedBy FOREIGN KEY (DeletedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT CK_Patients_Gender CHECK (Gender IS NULL OR Gender IN (N'Male', N'Female', N'Other')),
    CONSTRAINT CK_Patients_BloodType CHECK (BloodType IS NULL OR BloodType IN (N'A+',N'A-',N'B+',N'B-',N'AB+',N'AB-',N'O+',N'O-'))
);
GO

-- Active patients only: allow re-use of NationalID after soft delete
CREATE UNIQUE INDEX UX_Patients_NationalID_Active
    ON dbo.Patients(NationalID)
    WHERE NationalID IS NOT NULL AND IsDeleted = 0;
GO

CREATE INDEX IX_Patients_Phone ON dbo.Patients(Phone) WHERE IsDeleted = 0;
CREATE INDEX IX_Patients_Name ON dbo.Patients(LastName, FirstName) WHERE IsDeleted = 0;
GO

-- Store files on disk/S3 — NOT inside SQL (performance + backup size)
CREATE TABLE dbo.PatientFiles (
    FileID          INT IDENTITY(1,1) NOT NULL,
    PatientID       INT NOT NULL,
    FileName        NVARCHAR(260) NOT NULL,
    FileType        NVARCHAR(50)  NOT NULL,
    StoragePath     NVARCHAR(500) NOT NULL,
    FileHashSha256  CHAR(64)      NULL,
    FileSizeBytes   BIGINT        NULL,
    Description     NVARCHAR(500) NULL,
    IsDeleted       BIT NOT NULL CONSTRAINT DF_PatientFiles_IsDeleted DEFAULT (0),
    UploadedAt      DATETIME2(0) NOT NULL CONSTRAINT DF_PatientFiles_UploadedAt DEFAULT (SYSUTCDATETIME()),
    UploadedBy      INT NULL,
    CONSTRAINT PK_PatientFiles PRIMARY KEY CLUSTERED (FileID),
    CONSTRAINT FK_PatientFiles_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_PatientFiles_UploadedBy FOREIGN KEY (UploadedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.MedicalHistory (
    RecordID        INT IDENTITY(1,1) NOT NULL,
    PatientID       INT NOT NULL,
    VisitDate       DATETIME2(0) NOT NULL,
    DoctorID        INT NULL,
    BranchID        INT NULL,
    Notes           NVARCHAR(MAX) NULL,
    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_MedicalHistory_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy       INT NULL,
    CONSTRAINT PK_MedicalHistory PRIMARY KEY CLUSTERED (RecordID),
    CONSTRAINT FK_MedicalHistory_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_MedicalHistory_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT FK_MedicalHistory_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID),
    CONSTRAINT FK_MedicalHistory_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);
GO

-- =============================================
-- 4) Appointments & clinical visit
-- =============================================

CREATE TABLE dbo.Appointments (
    AppointmentID       INT IDENTITY(1,1) NOT NULL,
    PatientID           INT NOT NULL,
    DoctorID            INT NOT NULL,
    BranchID            INT NOT NULL,
    AppointmentStart    DATETIME2(0) NOT NULL,
    AppointmentEnd      DATETIME2(0) NOT NULL,
    QueueNumber         INT NULL,
    Status              NVARCHAR(20) NOT NULL CONSTRAINT DF_Appointments_Status DEFAULT (N'Pending'),
    Notes               NVARCHAR(MAX) NULL,
    CancellationReason  NVARCHAR(200) NULL,
    CreatedAt           DATETIME2(0) NOT NULL CONSTRAINT DF_Appointments_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy           INT NULL,
    UpdatedAt           DATETIME2(0) NULL,
    UpdatedBy           INT NULL,
    CONSTRAINT PK_Appointments PRIMARY KEY CLUSTERED (AppointmentID),
    CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT FK_Appointments_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID),
    CONSTRAINT FK_Appointments_DoctorBranches FOREIGN KEY (DoctorID, BranchID) REFERENCES dbo.DoctorBranches(DoctorID, BranchID),
    CONSTRAINT FK_Appointments_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT CK_Appointments_Time CHECK (AppointmentEnd > AppointmentStart),
    CONSTRAINT CK_Appointments_Status CHECK (Status IN (
        N'Pending', N'Confirmed', N'CheckedIn', N'InProgress',
        N'Completed', N'Cancelled', N'NoShow'
    ))
);
GO

-- Prevent double booking (filtered index: no NOT IN — use positive list only)
CREATE UNIQUE INDEX UX_Appointments_DoctorStart_Active
    ON dbo.Appointments(DoctorID, AppointmentStart)
    WHERE Status IN (N'Pending', N'Confirmed', N'CheckedIn', N'InProgress', N'Completed');
GO

CREATE INDEX IX_Appointments_DoctorDate ON dbo.Appointments(DoctorID, AppointmentStart) INCLUDE (Status, BranchID, PatientID);
CREATE INDEX IX_Appointments_Patient ON dbo.Appointments(PatientID, AppointmentStart);
CREATE INDEX IX_Appointments_BranchDate ON dbo.Appointments(BranchID, AppointmentStart);
GO

CREATE TABLE dbo.Diagnoses (
    DiagnosisID     INT IDENTITY(1,1) NOT NULL,
    AppointmentID   INT NOT NULL,
    DoctorID        INT NOT NULL,
    DiagnosisText   NVARCHAR(MAX) NOT NULL,
    Notes           NVARCHAR(MAX) NULL,
    DiagnosisDate   DATETIME2(0) NOT NULL CONSTRAINT DF_Diagnoses_DiagnosisDate DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_Diagnoses PRIMARY KEY CLUSTERED (DiagnosisID),
    CONSTRAINT FK_Diagnoses_Appointments FOREIGN KEY (AppointmentID) REFERENCES dbo.Appointments(AppointmentID),
    CONSTRAINT FK_Diagnoses_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT UQ_Diagnoses_Appointment UNIQUE (AppointmentID)
);
GO

CREATE TABLE dbo.Prescriptions (
    PrescriptionID      INT IDENTITY(1,1) NOT NULL,
    AppointmentID       INT NOT NULL,
    DoctorID            INT NOT NULL,
    PrescriptionText    NVARCHAR(MAX) NOT NULL,
    FollowUpDate        DATE NULL,
    CreatedAt           DATETIME2(0) NOT NULL CONSTRAINT DF_Prescriptions_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_Prescriptions PRIMARY KEY CLUSTERED (PrescriptionID),
    CONSTRAINT FK_Prescriptions_Appointments FOREIGN KEY (AppointmentID) REFERENCES dbo.Appointments(AppointmentID),
    CONSTRAINT FK_Prescriptions_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID)
);
GO

-- =============================================
-- 5) Billing (services, invoices, payments) — Egypt/Gulf ready
-- =============================================

CREATE TABLE dbo.Services (
    ServiceID       INT IDENTITY(1,1) NOT NULL,
    ServiceCode     NVARCHAR(30) NOT NULL,
    ServiceName     NVARCHAR(150) NOT NULL,
    DefaultPrice    DECIMAL(18,2) NOT NULL,
    IsActive        BIT NOT NULL CONSTRAINT DF_Services_IsActive DEFAULT (1),
    CONSTRAINT PK_Services PRIMARY KEY CLUSTERED (ServiceID),
    CONSTRAINT UQ_Services_Code UNIQUE (ServiceCode),
    CONSTRAINT CK_Services_Price CHECK (DefaultPrice >= 0)
);
GO

CREATE TABLE dbo.Invoices (
    InvoiceID       INT IDENTITY(1,1) NOT NULL,
    InvoiceNo       NVARCHAR(30) NOT NULL,
    PatientID       INT NOT NULL,
    AppointmentID   INT NULL,
    BranchID        INT NOT NULL,
    InvoiceDate     DATETIME2(0) NOT NULL CONSTRAINT DF_Invoices_InvoiceDate DEFAULT (SYSUTCDATETIME()),
    CurrencyCode    CHAR(3) NOT NULL CONSTRAINT DF_Invoices_Currency DEFAULT ('EGP'),
    SubTotal        DECIMAL(18,2) NOT NULL CONSTRAINT DF_Invoices_SubTotal DEFAULT (0),
    DiscountAmount  DECIMAL(18,2) NOT NULL CONSTRAINT DF_Invoices_Discount DEFAULT (0),
    TaxRate         DECIMAL(5,2)  NOT NULL CONSTRAINT DF_Invoices_TaxRate DEFAULT (14.00),
    TaxAmount       DECIMAL(18,2) NOT NULL CONSTRAINT DF_Invoices_TaxAmount DEFAULT (0),
    NetAmount       DECIMAL(18,2) NOT NULL,
    Status          NVARCHAR(20) NOT NULL CONSTRAINT DF_Invoices_Status DEFAULT (N'Open'),
    Notes           NVARCHAR(500) NULL,
    CreatedBy       INT NULL,
    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_Invoices_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_Invoices PRIMARY KEY CLUSTERED (InvoiceID),
    CONSTRAINT UQ_Invoices_InvoiceNo UNIQUE (InvoiceNo),
    CONSTRAINT FK_Invoices_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_Invoices_Appointments FOREIGN KEY (AppointmentID) REFERENCES dbo.Appointments(AppointmentID),
    CONSTRAINT FK_Invoices_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID),
    CONSTRAINT FK_Invoices_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT CK_Invoices_Amounts CHECK (
        SubTotal >= 0 AND DiscountAmount >= 0 AND TaxAmount >= 0 AND NetAmount >= 0
        AND DiscountAmount <= SubTotal
    ),
    CONSTRAINT CK_Invoices_Status CHECK (Status IN (N'Open', N'PartiallyPaid', N'Paid', N'Cancelled', N'Refunded'))
);
GO

CREATE TABLE dbo.InvoiceLines (
    LineID          INT IDENTITY(1,1) NOT NULL,
    InvoiceID       INT NOT NULL,
    ServiceID       INT NULL,
    Description     NVARCHAR(200) NOT NULL,
    Quantity        DECIMAL(10,2) NOT NULL CONSTRAINT DF_InvoiceLines_Qty DEFAULT (1),
    UnitPrice       DECIMAL(18,2) NOT NULL,
    LineTotal       AS (Quantity * UnitPrice) PERSISTED,
    CONSTRAINT PK_InvoiceLines PRIMARY KEY CLUSTERED (LineID),
    CONSTRAINT FK_InvoiceLines_Invoices FOREIGN KEY (InvoiceID) REFERENCES dbo.Invoices(InvoiceID) ON DELETE CASCADE,
    CONSTRAINT FK_InvoiceLines_Services FOREIGN KEY (ServiceID) REFERENCES dbo.Services(ServiceID),
    CONSTRAINT CK_InvoiceLines_Qty CHECK (Quantity > 0),
    CONSTRAINT CK_InvoiceLines_Price CHECK (UnitPrice >= 0)
);
GO

CREATE TABLE dbo.Payments (
    PaymentID       INT IDENTITY(1,1) NOT NULL,
    InvoiceID       INT NOT NULL,
    Amount          DECIMAL(18,2) NOT NULL,
    PaymentDate     DATETIME2(0) NOT NULL CONSTRAINT DF_Payments_PaymentDate DEFAULT (SYSUTCDATETIME()),
    PaymentMethod   NVARCHAR(30) NOT NULL,
    ReferenceNo     NVARCHAR(50) NULL,
    CreatedBy       INT NULL,
    CONSTRAINT PK_Payments PRIMARY KEY CLUSTERED (PaymentID),
    CONSTRAINT FK_Payments_Invoices FOREIGN KEY (InvoiceID) REFERENCES dbo.Invoices(InvoiceID),
    CONSTRAINT FK_Payments_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT CK_Payments_Amount CHECK (Amount > 0),
    CONSTRAINT CK_Payments_Method CHECK (PaymentMethod IN (
        N'Cash', N'Card', N'InstaPay', N'VodafoneCash', N'BankTransfer', N'Insurance', N'Other'
    ))
);
GO

CREATE TABLE dbo.ExpenseCategories (
    CategoryID      INT IDENTITY(1,1) NOT NULL,
    CategoryName    NVARCHAR(100) NOT NULL,
    IsActive        BIT NOT NULL CONSTRAINT DF_ExpenseCategories_IsActive DEFAULT (1),
    CONSTRAINT PK_ExpenseCategories PRIMARY KEY CLUSTERED (CategoryID),
    CONSTRAINT UQ_ExpenseCategories_Name UNIQUE (CategoryName)
);
GO

CREATE TABLE dbo.Expenses (
    ExpenseID       INT IDENTITY(1,1) NOT NULL,
    BranchID        INT NOT NULL,
    CategoryID      INT NULL,
    Amount          DECIMAL(18,2) NOT NULL,
    ExpenseDate     DATE NOT NULL,
    Description     NVARCHAR(500) NULL,
    CreatedBy       INT NULL,
    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_Expenses_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_Expenses PRIMARY KEY CLUSTERED (ExpenseID),
    CONSTRAINT FK_Expenses_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID),
    CONSTRAINT FK_Expenses_Categories FOREIGN KEY (CategoryID) REFERENCES dbo.ExpenseCategories(CategoryID),
    CONSTRAINT FK_Expenses_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT CK_Expenses_Amount CHECK (Amount > 0)
);
GO

-- =============================================
-- 6) Audit & notifications
-- =============================================

CREATE TABLE dbo.AuditLogs (
    LogID           BIGINT IDENTITY(1,1) NOT NULL,
    UserID          INT NULL,
    ActionType      NVARCHAR(20) NOT NULL,
    TableName       NVARCHAR(100) NOT NULL,
    RecordID        INT NULL,
    OldValuesJson   NVARCHAR(MAX) NULL,
    NewValuesJson   NVARCHAR(MAX) NULL,
    IpAddress       NVARCHAR(45) NULL,
    MachineName     NVARCHAR(100) NULL,
    BranchID        INT NULL,
    ActionAt        DATETIME2(0) NOT NULL CONSTRAINT DF_AuditLogs_ActionAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_AuditLogs PRIMARY KEY CLUSTERED (LogID),
    CONSTRAINT FK_AuditLogs_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_AuditLogs_Branches FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID),
    CONSTRAINT CK_AuditLogs_Action CHECK (ActionType IN (N'Insert', N'Update', N'Delete', N'Login', N'Logout', N'FailedLogin'))
);
GO

CREATE INDEX IX_AuditLogs_ActionAt ON dbo.AuditLogs(ActionAt DESC);
CREATE INDEX IX_AuditLogs_User ON dbo.AuditLogs(UserID, ActionAt DESC);
GO

CREATE TABLE dbo.Notifications (
    NotificationID  INT IDENTITY(1,1) NOT NULL,
    UserID          INT NOT NULL,
    Title           NVARCHAR(200) NOT NULL,
    Message         NVARCHAR(MAX) NOT NULL,
    Severity        NVARCHAR(10) NOT NULL CONSTRAINT DF_Notifications_Severity DEFAULT (N'Info'),
    IsRead          BIT NOT NULL CONSTRAINT DF_Notifications_IsRead DEFAULT (0),
    ExpiresAt       DATETIME2(0) NULL,
    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_Notifications_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_Notifications PRIMARY KEY CLUSTERED (NotificationID),
    CONSTRAINT FK_Notifications_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT CK_Notifications_Severity CHECK (Severity IN (N'Info', N'Warning', N'Error'))
);
GO

CREATE INDEX IX_Notifications_UserUnread ON dbo.Notifications(UserID, IsRead, CreatedAt DESC);
GO

-- Clinic-wide settings (VAT, locale, lockout policy)
CREATE TABLE dbo.ClinicSettings (
    SettingKey      NVARCHAR(100) NOT NULL,
    SettingValue    NVARCHAR(500) NOT NULL,
    UpdatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_ClinicSettings_UpdatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedBy       INT NULL,
    CONSTRAINT PK_ClinicSettings PRIMARY KEY CLUSTERED (SettingKey),
    CONSTRAINT FK_ClinicSettings_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES dbo.Users(UserID)
);
GO

-- =============================================
-- Seed: roles, permissions, branch, services, admin placeholder
-- =============================================

INSERT INTO dbo.Roles (RoleCode, RoleName, Description) VALUES
 (N'ADMIN',      N'Admin',      N'مدير النظام'),
 (N'DOCTOR',     N'Doctor',     N'طبيب'),
 (N'RECEPTION',  N'Reception',  N'استقبال'),
 (N'ACCOUNTANT', N'Accountant', N'محاسب'),
 (N'BRANCHMGR',  N'BranchManager', N'مدير فرع');
GO

INSERT INTO dbo.Permissions (PermissionCode, PermissionName, ModuleName) VALUES
 (N'BRANCH_VIEW',     N'View branches',        N'Branches'),
 (N'BRANCH_MANAGE',   N'Manage branches',      N'Branches'),
 (N'USER_MANAGE',     N'Manage users',         N'Users'),
 (N'PATIENT_VIEW',    N'View patients',        N'Patients'),
 (N'PATIENT_MANAGE',  N'Manage patients',      N'Patients'),
 (N'APPT_VIEW',       N'View appointments',    N'Appointments'),
 (N'APPT_MANAGE',     N'Manage appointments',  N'Appointments'),
 (N'CLINICAL_WRITE',  N'Write clinical data',  N'Clinical'),
 (N'INVOICE_VIEW',    N'View invoices',        N'Billing'),
 (N'INVOICE_MANAGE',  N'Manage invoices',      N'Billing'),
 (N'PAYMENT_MANAGE',  N'Manage payments',      N'Billing'),
 (N'EXPENSE_MANAGE',  N'Manage expenses',      N'Billing'),
 (N'REPORT_VIEW',     N'View reports',         N'Reports'),
 (N'AUDIT_VIEW',      N'View audit logs',      N'System');
GO

-- Admin: all permissions
INSERT INTO dbo.RolePermissions (RoleID, PermissionID)
SELECT r.RoleID, p.PermissionID
FROM dbo.Roles r
CROSS JOIN dbo.Permissions p
WHERE r.RoleCode = N'ADMIN';
GO

-- Doctor: clinical + appointments + patients view
INSERT INTO dbo.RolePermissions (RoleID, PermissionID)
SELECT r.RoleID, p.PermissionID
FROM dbo.Roles r
JOIN dbo.Permissions p ON p.PermissionCode IN (
    N'PATIENT_VIEW', N'PATIENT_MANAGE', N'APPT_VIEW', N'APPT_MANAGE', N'CLINICAL_WRITE', N'REPORT_VIEW'
)
WHERE r.RoleCode = N'DOCTOR';
GO

INSERT INTO dbo.Specialties (SpecialtyName) VALUES
 (N'General Dentistry'), (N'Orthodontics'), (N'Endodontics'),
 (N'Periodontics'), (N'Oral Surgery');
GO

INSERT INTO dbo.Branches (BranchName, Address, Phone, OpenTime, CloseTime) VALUES
 (N'فرع السويس الرئيسي', N'شارع الجيش - السويس', N'01012345678', '09:00', '21:00');
GO

INSERT INTO dbo.Services (ServiceCode, ServiceName, DefaultPrice) VALUES
 (N'CONSULT', N'كشف', 300.00),
 (N'XRAY',    N'أشعة', 200.00),
 (N'FILLING', N'حشو', 500.00);
GO

INSERT INTO dbo.ExpenseCategories (CategoryName) VALUES
 (N'إيجار'), (N'مرتبات'), (N'مستلزمات'), (N'كهرباء ومياه');
GO

INSERT INTO dbo.ClinicSettings (SettingKey, SettingValue) VALUES
 (N'DefaultCountryCode', N'EG'),
 (N'DefaultCurrency', N'EGP'),
 (N'DefaultVatRate', N'14.00'),
 (N'MaxFailedLoginAttempts', N'5'),
 (N'LockoutMinutes', N'15'),
 (N'PatientFilesRoot', N'C:\ClinicData\PatientFiles');
GO

/*
 Placeholder hash — MUST be replaced on first run from VB.NET:
   BCrypt.Net.BCrypt.HashPassword("YourStrongPassword")
 Or run a one-time setup form.
*/
INSERT INTO dbo.Users (Username, PasswordHash, RoleID, FullName, MustChangePassword, IsActive)
SELECT N'admin',
       N'$2a$12$REPLACE_ON_FIRST_DEPLOY_WITH_REAL_BCRYPT_HASH',
       RoleID,
       N'مدير النظام',
       1,
       1
FROM dbo.Roles WHERE RoleCode = N'ADMIN';
GO

INSERT INTO dbo.UserBranches (UserID, BranchID, IsDefault)
SELECT u.UserID, b.BranchID, 1
FROM dbo.Users u
CROSS JOIN dbo.Branches b
WHERE u.Username = N'admin' AND b.BranchID = 1;
GO

PRINT N'ClinicDB schema v1.0 deployed. Set admin PasswordHash before go-live.';
GO

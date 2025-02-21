CREATE DATABASE QLDRONE
GO 
USE QLDRONE
GO

CREATE TABLE [Customer] (
    [CustomerID] CHAR(5) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Type] NVARCHAR(1) NOT NULL,
    [Phone] VARCHAR(10) NULL CHECK ([Phone] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    [Email] VARCHAR(50) NULL,
    [Address] NVARCHAR(100) NULL,
    PRIMARY KEY ([CustomerID])
);
GO

CREATE TABLE [Drone] (
    [DroneID] CHAR(5) NOT NULL,
    [Name] NVARCHAR(100) NULL,
    [Type] NVARCHAR(20) NULL,
    [RentalPrice] INT NULL CHECK ([RentalPrice] > 0),
    [PurchasePrice] INT NULL CHECK ([PurchasePrice] > 0),
    [Status] NVARCHAR(100) NULL,
    PRIMARY KEY ([DroneID])
);
GO

CREATE TABLE [Contract_] (
    [ContractID] CHAR(5) NOT NULL,
    [StartDate] DATETIME NULL,
    [EndDate] DATETIME NULL,
    [TotalValue] INT NULL CHECK ([TotalValue] > 0),
    [Status] NVARCHAR(100) NULL,
    [CustomerID] CHAR(5) NOT NULL,
    [TechnicianTeamID] CHAR(5) NOT NULL,
    [PromotionID] CHAR(5) NOT NULL,
    PRIMARY KEY ([ContractID]),
    FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]),
    FOREIGN KEY ([TechnicianTeamID]) REFERENCES [TechnicianTeam]([TechnicianTeamID]),
    FOREIGN KEY ([PromotionID]) REFERENCES [Promotion]([PromotionID])
);
GO

CREATE TABLE [Technician] (
    [TechnicianID] CHAR(5) NOT NULL,
    [Name] NVARCHAR(100) NULL,
    [Role] NVARCHAR(50) NULL,
    [Status] NVARCHAR(50) NULL,
    [TechnicianTeamID] CHAR(5) NOT NULL,
    PRIMARY KEY ([TechnicianID]),
    FOREIGN KEY ([TechnicianTeamID]) REFERENCES [TechnicianTeam]([TechnicianTeamID])
);
GO

CREATE TABLE [ContractDetail] (
    [DroneID] CHAR(5) NOT NULL,
    [ContractID] CHAR(5) NOT NULL,
    [Quantity] SMALLINT NOT NULL,
    [Price] INT NULL CHECK ([Price] > 0),
    PRIMARY KEY ([DroneID], [ContractID]),
    FOREIGN KEY ([DroneID]) REFERENCES [Drone]([DroneID]),
    FOREIGN KEY ([ContractID]) REFERENCES [Contract_]([ContractID])
);
GO

CREATE TABLE [Feedback] (
    [FeedbackID] CHAR(5) NOT NULL,
    [Rating] SMALLINT NULL CHECK ([Rating] <= 5),
    [Comment] NVARCHAR(100) NULL,
    [DroneID] CHAR(5) NOT NULL,
    [ContractID] CHAR(5) NOT NULL,
    PRIMARY KEY ([FeedbackID]),
    FOREIGN KEY ([DroneID]) REFERENCES [Drone]([DroneID]),
    FOREIGN KEY ([ContractID]) REFERENCES [Contract_]([ContractID])
);
GO

CREATE TABLE [DroneMaintenance] (
    [MaintenanceID] CHAR(5) NOT NULL,
    [Date] DATETIME NULL,
    [Type] NVARCHAR(50) NULL,
    [Cost] INT NULL CHECK ([Cost] > 0),
    [DroneID] CHAR(5) NOT NULL,
    PRIMARY KEY ([MaintenanceID]),
    FOREIGN KEY ([DroneID]) REFERENCES [Drone]([DroneID])
);
GO

CREATE TABLE [Promotion] (
    [PromotionID] CHAR(5) NOT NULL,
    [Description] NVARCHAR(100) NULL,
    [DiscountRate] DECIMAL(5, 2) NULL,
    PRIMARY KEY ([PromotionID])
);
GO

CREATE TABLE [PenaltyTicket] (
    [TicketID] CHAR(5) NOT NULL,
    [PenaltyAmount] INT NULL,
    [Reason_] NVARCHAR(100) NULL,
    [DroneID] CHAR(5) NOT NULL,
    [ContractID] CHAR(5) NOT NULL,
    PRIMARY KEY ([TicketID]),
    FOREIGN KEY ([DroneID]) REFERENCES [Drone]([DroneID]),
    FOREIGN KEY ([ContractID]) REFERENCES [Contract_]([ContractID])
);
GO

CREATE TABLE [TechnicianTeam] (
    [TechnicianTeamID] CHAR(5) NOT NULL,
    [Name] NVARCHAR(100) NULL,
    PRIMARY KEY ([TechnicianTeamID])
);
GO

CREATE TABLE [CTMaintenance] (
    [TechnicianID] CHAR(5) NOT NULL,
    [MaintenanceID] CHAR(5) NOT NULL,
    PRIMARY KEY ([TechnicianID], [MaintenanceID]),
    FOREIGN KEY ([TechnicianID]) REFERENCES [Technician]([TechnicianID]),
    FOREIGN KEY ([MaintenanceID]) REFERENCES [DroneMaintenance]([MaintenanceID])
);
GO

-- Thêm dữ liệu vào Customer
INSERT INTO [Customer] ([CustomerID], [Name], [Type], [Phone], [Email], [Address]) VALUES
('C014', N'Vũ Thị Thanh', 'A', '0912121212', 'thanh@gmail.com', N'Hà Nội'),
('C015', N'Lê Văn Kiên', 'B', '0923232323', 'kien@gmail.com', N'Hải Phòng'),
('C016', N'Hoàng Đức Long', 'C', '0934343434', 'long@gmail.com', N'Hồ Chí Minh'),
('C017', N'Phạm Minh Huy', 'A', '0945454545', 'huy@gmail.com', N'Cần Thơ'),
('C018', N'Trần Thị Ngọc', 'B', '0956565656', 'ngoc@gmail.com', N'Nghệ An');

-- Thêm dữ liệu vào Drone
INSERT INTO [Drone] ([DroneID], [Name], [Type], [RentalPrice], [PurchasePrice], [Status]) VALUES
('D014', N'DJI Air 2S', 'Quadcopter', 250000, 30000000, N'Available'),
('D015', N'Autel EVO Lite+', 'Quadcopter', 260000, 32000000, N'Available'),
('D016', N'DJI FPV Combo', 'Racing', 270000, 35000000, N'Maintenance'),
('D017', N'PowerVision PowerEgg X', 'Egg-Shaped', 280000, 36000000, N'Available'),
('D018', N'Ryze Tello', 'Mini', 200000, 15000000, N'Available');

-- Thêm dữ liệu vào TechnicianTeam
INSERT INTO [TechnicianTeam] ([TechnicianTeamID], [Name]) VALUES
('TT007', N'Team Lambda'),
('TT008', N'Team Omicron');

-- Thêm dữ liệu vào Technician
INSERT INTO [Technician] ([TechnicianID], [Name], [Role], [Status], [TechnicianTeamID]) VALUES
('T014', N'Nguyễn Văn Dũng', N'Leader', N'Active', 'TT007'),
('T015', N'Phạm Hồng Nhung', N'Member', N'Active', 'TT007'),
('T016', N'Lê Văn Bảo', N'Member', N'Inactive', 'TT008'),
('T017', N'Trần Quang Lâm', N'Member', N'Active', 'TT008'),
('T018', N'Hồ Thị Mai', N'Member', N'Active', 'TT007');

-- Thêm dữ liệu vào Promotion
INSERT INTO [Promotion] ([PromotionID], [Description], [DiscountRate]) VALUES
('P011', N'Special Lunar New Year', 0.25),
('P012', N'Spring Festival Offer', 0.20),
('P013', N'Flash Sale', 0.18),
('P014', N'Member Loyalty Discount', 0.15),
('P015', N'Black Friday Deal', 0.30);

-- Thêm dữ liệu vào Contract_
INSERT INTO [Contract_] ([ContractID], [StartDate], [EndDate], [TotalValue], [Status], [CustomerID], [TechnicianTeamID], [PromotionID]) VALUES
('CT012', '2025-01-11', '2025-01-20', 7000000, N'Pending', 'C014', 'TT007', 'P011'),
('CT013', '2025-01-15', '2025-01-25', 8000000, N'Active', 'C015', 'TT008', 'P012'),
('CT014', '2025-01-20', '2025-01-30', 7500000, N'Completed', 'C016', 'TT007', 'P013'),
('CT015', '2025-01-25', '2025-02-05', 6500000, N'Completed', 'C017', 'TT008', 'P014'),
('CT016', '2025-02-01', '2025-02-10', 6000000, N'Active', 'C018', 'TT007', 'P015');

-- Thêm dữ liệu vào ContractDetail
INSERT INTO [ContractDetail] ([DroneID], [ContractID], [Quantity], [Price]) VALUES
('D014', 'CT012', 2, 500000),
('D015', 'CT013', 3, 780000),
('D016', 'CT014', 1, 270000),
('D017', 'CT015', 4, 1120000),
('D018', 'CT016', 2, 400000);

-- Thêm dữ liệu vào Feedback
INSERT INTO [Feedback] ([FeedbackID], [Rating], [Comment], [DroneID], [ContractID]) VALUES
('FB012', 5, N'Dịch vụ rất tốt.', 'D014', 'CT012'),
('FB013', 4, N'Chất lượng đạt yêu cầu.', 'D015', 'CT013'),
('FB014', 3, N'Cần cải thiện giao hàng.', 'D016', 'CT014'),
('FB015', 2, N'Drone không đúng như kỳ vọng.', 'D017', 'CT015'),
('FB016', 5, N'Rất hài lòng!', 'D018', 'CT016');

-- Thêm dữ liệu vào DroneMaintenance
INSERT INTO [DroneMaintenance] ([MaintenanceID], [Date], [Type], [Cost], [DroneID]) VALUES
('M009', '2025-01-05', N'Battery Replacement', 500000, 'D016'),
('M010', '2025-01-07', N'Camera Calibration', 700000, 'D017'),
('M011', '2025-01-10', N'Firmware Update', 200000, 'D018');

-- Thêm dữ liệu vào PenaltyTicket
INSERT INTO [PenaltyTicket] ([TicketID], [PenaltyAmount], [Reason_], [DroneID], [ContractID]) VALUES
('PT009', 300000, N'Hỏng động cơ', 'D016', 'CT014'),
('PT010', 250000, N'Hỏng cánh quạt', 'D017', 'CT015'),
('PT011', 200000, N'Pin bị chai', 'D018', 'CT016');

-- Thêm dữ liệu vào CTMaintenance
INSERT INTO [CTMaintenance] ([TechnicianID], [MaintenanceID]) VALUES
('T014', 'M009'),
('T015', 'M010'),
('T016', 'M011');

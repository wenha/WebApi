--前台用户
CREATE TABLE SysUser
(
    Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100) NOT NULL,
	CertNo NVARCHAR(20) NULL,
	Phone NVARCHAR(20) NULL,
	Sex TINYINT NULL,
	Password NVARCHAR(100) NOT NULL,
	AvatorImg NVARCHAR(200) NULL,
	AddTime DATETIME NOT NULL DEFAULT(GETDATE()),
	IsDelete BIT NOT NULL DEFAULT(0)
)

--后台用户
CREATE TABLE SysAccount
(
    Id INT PRIMARY KEY IDENTITY(1,1),
	LoginName NVARCHAR(200) NOT NULL,
	Name NVARCHAR(200) NULL,
	Password NVARCHAR(100) NULL,
	OrganId INT NOT NULL,
	AddTime DATETIME DEFAULT(GETDATE()),
	IsDelete BIT NOT NULL DEFAULT(0)
)

--机构
CREATE TABLE SysOrgan
(
    Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100) NOT NULL,
	ParentId INT NOT NULL DEFAULT(0),
	AddTime DATETIME NOT NULL DEFAULT(GETDATE()),
	IsDelete BIT NOT NULL DEFAULT(0)
)


--菜单页面
CREATE TABLE [dbo].[SysMenu](
	[Id] [tinyint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Level] [tinyint] NOT NULL,
	[ParentId] [tinyint] NOT NULL DEFAULT(0),
	IsDelete BIT NOT NULL DEFAULT(0)
)

--角色
CREATE TABLE [dbo].[SysRole](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](20) NULL,
)

--角色菜单关联表
CREATE TABLE [dbo].[SysRoleMenu](
	[RoleId] [tinyint] NOT NULL,
	[MenuId] [tinyint] NOT NULL,
)

--管理员角色表
CREATE TABLE SysAccountRole
(
    AccountId INT NOT NULL,
	RoleId TINYINT NOT NULL
)
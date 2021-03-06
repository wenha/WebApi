USE [WebApi]
GO
/****** Object:  Table [dbo].[SysAccount]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NULL,
	[OrganId] [int] NOT NULL,
	[AddTime] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysAccountRole]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysAccountRole](
	[AccountId] [int] NOT NULL,
	[RoleId] [tinyint] NOT NULL,
 CONSTRAINT [PK_SysAccountRole] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysMenu]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysMenu](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Level] [tinyint] NOT NULL,
	[ParentId] [tinyint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SysMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysOrgan]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysOrgan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ParentId] [int] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create table [dbo].[SysRole](
	[Id] [tinyint] identity(1,1) not null,
	[Name] [nvarchar](50) not null,
	[Description] [nvarchar](20) null,
	[IsDelete] [bit] not null,
 constraint [PK_SysRole] primary key clustered 
(
	[Id] asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]

go
GO
/****** Object:  Table [dbo].[SysRoleMenu]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRoleMenu](
	[RoleId] [tinyint] NOT NULL,
	[MenuId] [tinyint] NOT NULL,
 CONSTRAINT [PK_SysRoleMenu] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2019/7/1 17:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CertNo] [nvarchar](20) NULL,
	[Phone] [nvarchar](20) NULL,
	[Sex] [tinyint] NULL,
	[Password] [nvarchar](100) NOT NULL,
	[AvatorImg] [nvarchar](200) NULL,
	[AddTime] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SysAccount] ADD  DEFAULT (getdate()) FOR [AddTime]
GO
ALTER TABLE [dbo].[SysAccount] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysMenu] ADD  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[SysMenu] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysOrgan] ADD  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[SysOrgan] ADD  DEFAULT (getdate()) FOR [AddTime]
GO
ALTER TABLE [dbo].[SysOrgan] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SysUser] ADD  DEFAULT (getdate()) FOR [AddTime]
GO
ALTER TABLE [dbo].[SysUser] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
alter table [dbo].[SysRole] add  constraint [DF_SysRole_IsDelete]  default ((0)) for [IsDelete]
go
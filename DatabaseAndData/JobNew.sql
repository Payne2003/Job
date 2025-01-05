USE [JobNew]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 06/01/2025 1:14:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[CandidateId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Cv] [nvarchar](max) NULL,
	[countryside] [nvarchar](max) NULL,
	[Currentjob] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[OtpCode] [nvarchar](6) NULL,
	[OtpExpiryTime] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[Avatar] [nvarchar](255) NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[CandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Telephone] [nvarchar](15) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CV]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CandidateId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[DateOfBirth] [nvarchar](max) NULL,
	[BusinessCard] [nvarchar](max) NULL,
	[CareerObjective] [nvarchar](max) NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[WorkExperience_CompanyName] [nvarchar](max) NULL,
	[WorkExperience_Position] [nvarchar](max) NULL,
	[WorkExperience_StartDate] [nvarchar](max) NULL,
	[WorkExperience_EndDate] [nvarchar](max) NULL,
	[WorkExperience_Description] [nvarchar](max) NULL,
	[Education_SchoolName] [nvarchar](max) NULL,
	[Education_Degree] [nvarchar](max) NULL,
	[Education_StartDate] [nvarchar](max) NULL,
	[Education_EndDate] [nvarchar](max) NULL,
	[Education_Major] [nvarchar](max) NULL,
	[SkillsName] [nvarchar](max) NULL,
	[Skills] [nvarchar](max) NULL,
	[Certificate_CertificateName] [nvarchar](max) NULL,
	[Certificate_IssuedDate] [nvarchar](max) NULL,
	[Project_ProjectName] [nvarchar](max) NULL,
	[Project_Description] [nvarchar](max) NULL,
	[Project_Role] [nvarchar](max) NULL,
	[Project_StartDate] [nvarchar](max) NULL,
	[Project_EndDate] [nvarchar](max) NULL,
	[Activity_ActivityName] [nvarchar](max) NULL,
	[Activity_Description] [nvarchar](max) NULL,
	[Hobbies] [nvarchar](max) NULL,
	[Reference_Name] [nvarchar](max) NULL,
	[Reference_Position] [nvarchar](max) NULL,
	[Reference_Company] [nvarchar](max) NULL,
	[Reference_ContactInfo] [nvarchar](max) NULL,
	[Award_AwardName] [nvarchar](max) NULL,
	[Award_Date] [nvarchar](max) NULL,
	[CreateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CvTemplate]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CvTemplate](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [nvarchar](max) NOT NULL,
	[HtmlEditContent] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[HtmlCreateContent] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CvTemplate] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Experience] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[RecruiterId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ExpiredAt] [datetime2](7) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[LocationId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Link]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link](
	[LinkId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[JobId] [int] NULL,
	[CandidateId] [int] NULL,
	[RecruiterId] [int] NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[LinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Province] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recruiter]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recruiter](
	[RecruiterId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[OtpCode] [nvarchar](6) NULL,
	[OtpExpiryTime] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[Avatar] [nvarchar](255) NULL,
	[Gender] [int] NULL,
	[countryside] [nvarchar](max) NULL,
	[Currentjob] [nvarchar](max) NULL,
 CONSTRAINT [PK_Recruiter] PRIMARY KEY CLUSTERED 
(
	[RecruiterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resume]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resume](
	[JobId] [int] NOT NULL,
	[CandidateId] [int] NOT NULL,
	[Cv] [nvarchar](max) NOT NULL,
	[Introduce] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Resume] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[CandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SavedJob]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavedJob](
	[JobId] [int] NOT NULL,
	[CandidateId] [int] NOT NULL,
	[SavedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SavedJob] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[CandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 06/01/2025 1:14:02 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[JobId] [int] NULL,
	[CandidateId] [int] NULL,
	[RecruiterId] [int] NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CV] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [dbo].[CvTemplate] ADD  DEFAULT (N'') FOR [HtmlCreateContent]
GO
ALTER TABLE [dbo].[CV]  WITH CHECK ADD  CONSTRAINT [FK_CV_Candidate_CandidateId] FOREIGN KEY([CandidateId])
REFERENCES [dbo].[Candidate] ([CandidateId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CV] CHECK CONSTRAINT [FK_CV_Candidate_CandidateId]
GO
ALTER TABLE [dbo].[CV]  WITH CHECK ADD  CONSTRAINT [FK_CV_CvTemplate_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[CvTemplate] ([TemplateId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CV] CHECK CONSTRAINT [FK_CV_CvTemplate_TemplateId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Category_CategoryId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Location_LocationId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Recruiter_RecruiterId] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Recruiter_RecruiterId]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_Candidate_CandidateId] FOREIGN KEY([CandidateId])
REFERENCES [dbo].[Candidate] ([CandidateId])
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_Candidate_CandidateId]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_Job_JobId]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_Recruiter_RecruiterId] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_Recruiter_RecruiterId]
GO
ALTER TABLE [dbo].[Resume]  WITH CHECK ADD  CONSTRAINT [FK_Resume_Candidate_CandidateId] FOREIGN KEY([CandidateId])
REFERENCES [dbo].[Candidate] ([CandidateId])
GO
ALTER TABLE [dbo].[Resume] CHECK CONSTRAINT [FK_Resume_Candidate_CandidateId]
GO
ALTER TABLE [dbo].[Resume]  WITH CHECK ADD  CONSTRAINT [FK_Resume_Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[Resume] CHECK CONSTRAINT [FK_Resume_Job_JobId]
GO
ALTER TABLE [dbo].[SavedJob]  WITH CHECK ADD  CONSTRAINT [FK_SavedJob_Candidate_CandidateId] FOREIGN KEY([CandidateId])
REFERENCES [dbo].[Candidate] ([CandidateId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SavedJob] CHECK CONSTRAINT [FK_SavedJob_Candidate_CandidateId]
GO
ALTER TABLE [dbo].[SavedJob]  WITH CHECK ADD  CONSTRAINT [FK_SavedJob_Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SavedJob] CHECK CONSTRAINT [FK_SavedJob_Job_JobId]
GO
ALTER TABLE [dbo].[Skill]  WITH CHECK ADD  CONSTRAINT [FK_Skill_Candidate_CandidateId] FOREIGN KEY([CandidateId])
REFERENCES [dbo].[Candidate] ([CandidateId])
GO
ALTER TABLE [dbo].[Skill] CHECK CONSTRAINT [FK_Skill_Candidate_CandidateId]
GO
ALTER TABLE [dbo].[Skill]  WITH CHECK ADD  CONSTRAINT [FK_Skill_Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Skill] CHECK CONSTRAINT [FK_Skill_Job_JobId]
GO
ALTER TABLE [dbo].[Skill]  WITH CHECK ADD  CONSTRAINT [FK_Skill_Recruiter_RecruiterId] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
ALTER TABLE [dbo].[Skill] CHECK CONSTRAINT [FK_Skill_Recruiter_RecruiterId]
GO

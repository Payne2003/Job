USE [JobNew]
GO
SET IDENTITY_INSERT [dbo].[Candidate] ON 
GO
INSERT [dbo].[Candidate] ([CandidateId], [UserName], [Email], [Password], [PhoneNumber], [Cv], [countryside], [Currentjob], [Company], [EmailConfirmed], [OtpCode], [OtpExpiryTime], [CreatedAt], [UpdatedAt], [Avatar], [Gender]) VALUES (30, N'duy123', N'lahuuduy1234@gmail.com', N'$2a$11$9eTN0.XiOmQ7aDNRWoNcHewJlz09.O7c3o79ZQ9qnnoDhvV0YAMV2', N'0387559262', N'kt.drawio.png', N'Ninh Bình', N'Student', N'utc', 1, N'418784', CAST(N'2024-10-02T21:25:00.9910773' AS DateTime2), CAST(N'2024-09-30T17:47:55.9192812' AS DateTime2), CAST(N'2024-10-09T22:13:59.9966679' AS DateTime2), N'u1.jpg', 0)
GO
INSERT [dbo].[Candidate] ([CandidateId], [UserName], [Email], [Password], [PhoneNumber], [Cv], [countryside], [Currentjob], [Company], [EmailConfirmed], [OtpCode], [OtpExpiryTime], [CreatedAt], [UpdatedAt], [Avatar], [Gender]) VALUES (35, N'CNTT', N'khanhvu001133@gmail.com', N'$2a$11$xRmzfxZBvxUUYoWVki4OhekB2lF7bxVWD1eFzsfyFw91dyUbPASPa', N'0387559262', N'BTL1.docx', N'Hà Nội', N'Student', NULL, 1, NULL, NULL, CAST(N'2024-12-01T20:56:43.5254359' AS DateTime2), CAST(N'2024-12-01T20:56:43.5254363' AS DateTime2), N'u3.jpg', 0)
GO
SET IDENTITY_INSERT [dbo].[Candidate] OFF
GO
SET IDENTITY_INSERT [dbo].[CvTemplate] ON 
GO
INSERT [dbo].[CvTemplate] ([TemplateId], [TemplateName], [HtmlEditContent], [Image], [HtmlCreateContent]) VALUES (1, N'Mẫu 1', N'/CvTemplates/Template1/Etemplate1.html', N'1.jpg', N'/CvTemplates/Template1/template1.html')
GO
INSERT [dbo].[CvTemplate] ([TemplateId], [TemplateName], [HtmlEditContent], [Image], [HtmlCreateContent]) VALUES (3, N'Mẫu 2', N'/CvTemplates/Template2/Etemplate2.html', N'2.jpg', N'/CvTemplates/Template2/template2.html')
GO
INSERT [dbo].[CvTemplate] ([TemplateId], [TemplateName], [HtmlEditContent], [Image], [HtmlCreateContent]) VALUES (5, N'Mẫu 3', N'/CvTemplates/Template3/Etemplate3.html', N'3.jpg', N'/CvTemplates/Template3/template3.html')
GO
INSERT [dbo].[CvTemplate] ([TemplateId], [TemplateName], [HtmlEditContent], [Image], [HtmlCreateContent]) VALUES (6, N'Mẫu 4', N'/CvTemplates/Template4/Etemplate4.html', N'4.jpg', N'/CvTemplates/Template4/template4.html')
GO
SET IDENTITY_INSERT [dbo].[CvTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[CV] ON 
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (9, 30, 6, N'Lã Hữu Duy', N'non', N'LO', N'0387559262', N'lahuuduy1234@gmail.comh', NULL, N'iam ', N'1', N'while1.jpg', N'1', N'1', N'2024-11-29', N'2024-11-29', N'1', N'1', N'1', N'2024-11-27', N'2024-11-28', N'1', N'1', N'1', NULL, NULL, N'1', N'1', N'1', N'2024-11-26', N'2024-11-29', N'11', N'1', N'1', N'1', N'1', N'1', N'1', N'1', N'2024-11-28', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (22, 30, 6, N'duy', N'duy', NULL, NULL, N'lahuuduy1234@gmail.com', NULL, NULL, NULL, N'1.jpg', NULL, NULL, N'2024-11-06', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (23, 30, 1, N'Lã Hữu Duy', N'1', NULL, NULL, N'lahuuduy1234@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (34, 30, 3, N'1', N'ded', NULL, NULL, N'trantuan120803@gmail.com', N'2024-11-26', NULL, NULL, N'1.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-20T19:56:14.2191268' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (35, 30, 3, N'1', N'ded', N'Ninh Bình12', N'03875592621', N'trantuan120803@gmail.com', N'2024-11-05', N'1', NULL, NULL, NULL, N'1', N'2024-11-28', N'2024-11-30', N'1', N'1', N'1', N'2024-11-28', N'2024-12-06', N'1', N'11', N'da', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-20T19:57:24.9425394' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (36, 30, 3, N'1', N'ded', NULL, NULL, N'kvthat@gmail.com', NULL, NULL, NULL, NULL, N'ứ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-20T19:59:34.4759180' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (37, 30, 5, N'1', N'ded', N'Ninh Bình1', N'0387559262', N'lahuuduy1234@gmail.com', N'a', NULL, NULL, NULL, N'ád', N'ád', N'2024-11-29', N'2024-11-23', N'ád', N'đa', N'ads', N'2024-11-29', N'2024-11-29', N'ad', NULL, N'á', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-20T20:12:52.3491554' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (38, 30, 6, N'duy', N'ded', N'số nhà 6', N'da', N'hhungzspo2003@gmail.com', NULL, N'dsad', N'ádasd', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:14:58.9715614' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (39, 30, 6, N'duy12', N'ded', N'số nhà 6', N'0387559262h', N'kvthat@gmail.com', NULL, N'sadsa', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:15:45.7054654' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (40, 30, 6, N'duy12', N'ded', N'số nhà 6', N'03875592621', N'hhungzspo2003@gmail.com', NULL, N'sdsfds', N'fsfsdf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:16:51.7957418' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (41, 30, 6, N'duy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:18:46.4009616' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (42, 30, 6, N'1', N'ded', N'số nhà 6', N'12', N'hhungzspo2003@gmail.com', NULL, N'dsfd', N'fsdfsdfds', NULL, N'sa', N'đ', N'2024-11-06', N'2024-11-14', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:21:10.4125323' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (43, 30, 6, N'duy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:23:35.8766581' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (44, 30, 6, N'duy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T19:26:19.3812535' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (45, 30, 6, N'duy', N'ded', N'số nhà 6', N'0387559262h', N'hhungzspo2003@gmail.com', NULL, N'đấ', NULL, NULL, N'á', N'ád', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T21:18:23.3867833' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (46, 30, 1, N'duy12', N'ấ', NULL, NULL, N'lahuuduy1234@gmail.com', NULL, N'd', N'212', N'while1.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2024-11-22', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-21T21:18:54.2781423' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (47, 30, 1, N'duy', N'ghgjgjgj', NULL, NULL, N'lahuuduy1234@gmail.com', NULL, N'12', NULL, N'LogoJobb.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-25T23:18:21.1509700' AS DateTime2))
GO
INSERT [dbo].[CV] ([ID], [CandidateId], [TemplateId], [Name], [FullName], [Address], [PhoneNumber], [Email], [DateOfBirth], [BusinessCard], [CareerObjective], [ProfilePicture], [WorkExperience_CompanyName], [WorkExperience_Position], [WorkExperience_StartDate], [WorkExperience_EndDate], [WorkExperience_Description], [Education_SchoolName], [Education_Degree], [Education_StartDate], [Education_EndDate], [Education_Major], [SkillsName], [Skills], [Certificate_CertificateName], [Certificate_IssuedDate], [Project_ProjectName], [Project_Description], [Project_Role], [Project_StartDate], [Project_EndDate], [Activity_ActivityName], [Activity_Description], [Hobbies], [Reference_Name], [Reference_Position], [Reference_Company], [Reference_ContactInfo], [Award_AwardName], [Award_Date], [CreateAt]) VALUES (49, 35, 6, N'Xin việc làm IT', N'Vũ Văn Khánh', N'Hà Nội', N'0387559262', N'khanhvu001133@gmail.com', NULL, NULL, NULL, N'u3.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-12-01T22:03:26.2276214' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[CV] OFF
GO
SET IDENTITY_INSERT [dbo].[Recruiter] ON 
GO
INSERT [dbo].[Recruiter] ([RecruiterId], [UserName], [Email], [Password], [PhoneNumber], [Company], [EmailConfirmed], [OtpCode], [OtpExpiryTime], [CreatedAt], [UpdatedAt], [Avatar], [Gender], [countryside], [Currentjob]) VALUES (10, N'duy12345', N'laduy28032003@gmail.com', N'$2a$11$4FjSwDlt3ZBbVok2f3eo1OQ1UztnbbiX0WS6jf12aVtG.VxGZjWza', N'03875592621', N'non', 1, NULL, NULL, CAST(N'2024-10-01T16:56:53.1344203' AS DateTime2), CAST(N'2024-10-10T00:50:27.9713402' AS DateTime2), N'r1.jpg', 1, N'Hà Nội', N'Team Leader')
GO
SET IDENTITY_INSERT [dbo].[Recruiter] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([CategoryId], [Name]) VALUES (1, N'Software Development')
GO
INSERT [dbo].[Category] ([CategoryId], [Name]) VALUES (2, N'Human Resources')
GO
INSERT [dbo].[Category] ([CategoryId], [Name]) VALUES (3, N'Marketing')
GO
INSERT [dbo].[Category] ([CategoryId], [Name]) VALUES (4, N'Sales')
GO
INSERT [dbo].[Category] ([CategoryId], [Name]) VALUES (5, N'Finance')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([LocationId], [Province]) VALUES (1, N'Hà Nội')
GO
INSERT [dbo].[Location] ([LocationId], [Province]) VALUES (2, N'Ninh Bình')
GO
INSERT [dbo].[Location] ([LocationId], [Province]) VALUES (3, N'TP.Hồ chí Minh')
GO
INSERT [dbo].[Location] ([LocationId], [Province]) VALUES (4, N'Nam Định')
GO
INSERT [dbo].[Location] ([LocationId], [Province]) VALUES (5, N'Hải Phòng')
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Job] ON 
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (20, N'Công ty FX Sài Gòn đang tuyển Nhân viên Kế toán', N'- Quản lý và kiểm soát các khoản thu, chi.  - Lập báo cáo tài chính theo tháng/quý/năm.  - Thực hiện các công việc liên quan đến thuế', N'2', N'FX Sài Gòn', 1, CAST(1000.00 AS Decimal(18, 2)), 10, 5, CAST(N'2024-11-26T09:22:08.6873693' AS DateTime2), CAST(N'2024-11-29T09:19:00.0000000' AS DateTime2), N'eb41e74b-cb4d-493b-b660-ecb93e354ead.png', 3, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (21, N'Nhân viên Hành chính - Nhân sự.', N'- Quản lý hồ sơ nhân viên, tuyển dụng và đào tạo.  - Xử lý các công việc hành chính văn phòng.', N'2', N'AXA Group', 2, CAST(1100.00 AS Decimal(18, 2)), 10, 2, CAST(N'2024-11-26T09:35:03.4975680' AS DateTime2), CAST(N'2025-01-09T09:34:00.0000000' AS DateTime2), N'7ef97658-1ad1-45f4-aac7-8add4e67a50b.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (22, N'Nhân viên Bán hàng tại showroom Vinfast.', N'- Tư vấn sản phẩm cho khách hàng.  - Quản lý hàng hóa tại cửa hàng.', N'3', N'AXA Group', 2, CAST(1100.00 AS Decimal(18, 2)), 10, 4, CAST(N'2024-11-26T09:37:08.9876453' AS DateTime2), CAST(N'2025-01-09T09:34:00.0000000' AS DateTime2), N'e88c216e-0e55-4c29-8621-a63980d5681a.png', 3, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (23, N'Nhân viên Kỹ thuật Cơ khí.', N'- Sửa chữa, bảo trì máy móc.  - Thiết kế và cải tiến dây chuyền sản xuất', N'1', N'Nippon Telegraph & Telephone', 2, CAST(250.00 AS Decimal(18, 2)), 10, 2, CAST(N'2024-11-26T09:38:29.6405058' AS DateTime2), CAST(N'2025-01-09T09:34:00.0000000' AS DateTime2), N'9197023e-212c-41bd-adc3-f6f6f9a75c45.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (24, N'Lập trình viên Full-stack tại Cầu giấy.', N'- Phát triển và duy trì hệ thống website và ứng dụng.  - Tham gia phân tích yêu cầu và thiết kế hệ thống.  ', N'1', N'D2T Software', 2, CAST(400.00 AS Decimal(18, 2)), 10, 1, CAST(N'2024-11-26T09:44:30.0354223' AS DateTime2), CAST(N'2025-01-01T09:44:00.0000000' AS DateTime2), N'e01fd272-94d3-4b91-b57f-267be0ce5814.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (26, N'Nhân viên Chăm sóc khách hàng', N'- Tiếp nhận, xử lý yêu cầu và giải đáp thắc mắc của khách hàng.  - Theo dõi và hỗ trợ khách hàng sau bán hàng.', N'1', N'VINARES', 2, CAST(300.00 AS Decimal(18, 2)), 10, 4, CAST(N'2024-11-26T10:16:15.0430971' AS DateTime2), CAST(N'2025-01-09T10:15:00.0000000' AS DateTime2), N'9fccbf44-0eef-4b3a-a160-57fbe63a12b9.png', 3, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (27, N'Web developer', N'- Tham gia phát triển và bảo trì các dự án web của công ty.  - Thiết kế giao diện, xây dựng chức năng theo yêu cầu dự án.  - Tối ưu hóa tốc độ và trải nghiệm người dùng trên website.  - Phối hợp với các phòng ban để phát triển các tính năng mới.  - Thực hiện sửa lỗi và cập nhật các ứng dụng web định kỳ.', N'1', N'Arouka', 5, CAST(500.00 AS Decimal(18, 2)), 10, 1, CAST(N'2024-10-01T16:57:51.3455207' AS DateTime2), CAST(N'2024-12-14T16:57:00.0000000' AS DateTime2), N'5d07ff0f-e017-4002-b798-46c57b8b1a29.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (28, N'Game Tester', N'Chơi thử game và viết báo cáo cho game platform  Yêu cầu: Có niềm yêu thích chơi game, có khả năng kiên nhẫn  Đãi ngộ: Hỗ trợ phí ăn uống, phí xăng xe.', N'1', N'VIGO', 3, CAST(200.00 AS Decimal(18, 2)), 10, 1, CAST(N'2024-10-07T20:58:48.2422863' AS DateTime2), CAST(N'2024-11-30T20:57:00.0000000' AS DateTime2), N'80c53312-a86f-46e1-8f2c-0e1a487a31a8.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (29, N'Nhân viên Content Writer.', N'- Viết bài PR, bài quảng cáo sản phẩm/dịch vụ.  - Quản lý nội dung trên website và mạng xã hội.', N'1', N'ART PUZZLE', 3, CAST(300.00 AS Decimal(18, 2)), 10, 3, CAST(N'2024-10-07T20:59:42.1537774' AS DateTime2), CAST(N'2024-12-07T20:57:00.0000000' AS DateTime2), N'dcf62e22-4cbd-40b9-a85d-5a5658fa10de.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (30, N'Chuyên viên Tư vấn Khách hàng', N'- Tiếp nhận và tư vấn khách hàng qua điện thoại/email.  - Chăm sóc khách hàng sau bán hàng.', N'1', N'Đức Bích ', 3, CAST(800.00 AS Decimal(18, 2)), 10, 4, CAST(N'2024-11-26T09:28:36.8880134' AS DateTime2), CAST(N'2025-01-15T09:26:00.0000000' AS DateTime2), N'684c4430-883b-46b9-917c-66ea79b3e824.png', 1, 1)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (37, N'Nhà sáng tạo nội dung', N'Nghĩ ra các kịch bản mag nội dung đặc sắc lôi cuốn', N'2', N'Vss', 2, CAST(1200.00 AS Decimal(18, 2)), 10, 2, CAST(N'2024-12-02T17:00:59.7996075' AS DateTime2), CAST(N'2024-12-20T17:00:00.0000000' AS DateTime2), N'c449ef5e-b9f3-4dfe-aea5-94ba36243f61.png', 1, 0)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (38, N'HR', N'e;dlfdopjfasd', N'dfasdfasdf', N'qayne', 12, CAST(123212.00 AS Decimal(18, 2)), 10, 2, CAST(N'2024-12-06T13:24:05.8713719' AS DateTime2), CAST(N'2024-12-26T13:23:00.0000000' AS DateTime2), N'c912cb26-b245-4a7e-b219-08c4f2d7d1fb.JPG', 1, 0)
GO
INSERT [dbo].[Job] ([JobId], [Title], [Description], [Experience], [Company], [Quantity], [Salary], [RecruiterId], [CategoryId], [CreatedAt], [ExpiredAt], [Image], [LocationId], [Status]) VALUES (39, N'HR', N'SADád', N'dsaĐÁ', N'qayne', 12, CAST(123212.00 AS Decimal(18, 2)), 10, 3, CAST(N'2024-12-06T14:18:12.2142064' AS DateTime2), CAST(N'2024-12-26T14:16:00.0000000' AS DateTime2), N'c711484d-cc34-42ba-abcd-b28d4013b476.JPG', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Job] OFF
GO
SET IDENTITY_INSERT [dbo].[Link] ON 
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1007, N'https://maps.app.goo.gl/mQVLoaxacjVji3Hz6', N'Google Maps', NULL, 30, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1008, N'https://www.facebook.com/profile.php?id=100075063850580', N'Facebook', NULL, 30, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1009, N'https://www.youtube.com/@duyla8694', N'YouTube', NULL, 30, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1010, N'https://www.youtube.com/@duyla8694', N'YouTube', NULL, NULL, 10)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1011, N'https://www.facebook.com/profile.php?id=100075063850580', N'Facebook', NULL, NULL, 10)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1012, N'https://maps.app.goo.gl/mQVLoaxacjVji3Hz6', N'Google Maps', NULL, NULL, 10)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1013, N'https://www.instagram.com/', N'Instagram', NULL, 30, 10)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1015, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 20, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1016, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 21, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1017, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 22, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1018, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 23, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1019, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 24, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1021, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 26, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1022, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 27, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1023, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 28, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1024, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 29, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1025, N'https://maps.app.goo.gl/7Z7ZA5xmf99srnmb7', N'Google Maps', 30, NULL, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1028, N'https://maps.app.goo.gl/mQVLoaxacjVji3Hz6', N'Google Maps', NULL, 35, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1029, N'https://www.facebook.com/profile.php?id=100075063850580', N'Facebook', NULL, 35, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1030, N'https://www.youtube.com/@duyla8694', N'YouTube', NULL, 35, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1031, N'https://www.instagram.com/', N'Instagram', NULL, 35, NULL)
GO
INSERT [dbo].[Link] ([LinkId], [Description], [Name], [JobId], [CandidateId], [RecruiterId]) VALUES (1032, N'https://maps.app.goo.gl/E8L5mJGaRSZAQLAq6', N'Google Maps', 38, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Link] OFF
GO
INSERT [dbo].[Resume] ([JobId], [CandidateId], [Cv], [Introduce], [CreatedAt], [Status]) VALUES (21, 30, N'CĐ-CNPM-N10-Nhom12.docx', N'123', CAST(N'2024-11-26T11:57:36.7714191' AS DateTime2), 0)
GO
INSERT [dbo].[Resume] ([JobId], [CandidateId], [Cv], [Introduce], [CreatedAt], [Status]) VALUES (21, 35, N'Project1.docx', N'Tôi là Khánh đang quan tâm đến công việc này', CAST(N'2024-12-02T16:43:47.8130911' AS DateTime2), 0)
GO
INSERT [dbo].[Resume] ([JobId], [CandidateId], [Cv], [Introduce], [CreatedAt], [Status]) VALUES (23, 30, N'CĐ-CNPM-N10-Nhom12.docx', NULL, CAST(N'2024-11-30T22:49:42.2203107' AS DateTime2), 0)
GO
INSERT [dbo].[Resume] ([JobId], [CandidateId], [Cv], [Introduce], [CreatedAt], [Status]) VALUES (28, 30, N'BTL1.docx', N'tôi là duy', CAST(N'2024-11-29T14:29:00.3408999' AS DateTime2), 0)
GO
INSERT [dbo].[SavedJob] ([JobId], [CandidateId], [SavedAt]) VALUES (20, 30, CAST(N'2024-11-30T22:22:03.8276263' AS DateTime2))
GO
INSERT [dbo].[SavedJob] ([JobId], [CandidateId], [SavedAt]) VALUES (21, 30, CAST(N'2024-12-02T00:08:33.5772363' AS DateTime2))
GO
INSERT [dbo].[SavedJob] ([JobId], [CandidateId], [SavedAt]) VALUES (21, 35, CAST(N'2024-12-01T22:26:39.9192728' AS DateTime2))
GO
INSERT [dbo].[SavedJob] ([JobId], [CandidateId], [SavedAt]) VALUES (22, 35, CAST(N'2024-12-02T16:27:27.6028489' AS DateTime2))
GO
INSERT [dbo].[SavedJob] ([JobId], [CandidateId], [SavedAt]) VALUES (23, 30, CAST(N'2024-12-02T00:08:40.5590242' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Skill] ON 
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1004, N'Leader', N'70%', NULL, 30, 10)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1006, N'TeamWork', N'80%', NULL, 30, 10)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1007, N'WebDesign', N'60%', NULL, 30, 10)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1008, N'Backend Api', N'90%', NULL, 30, 10)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1009, N'Kinh nghiệm.', N'Ưu tiên ứng viên có kinh nghiệm.', 26, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1010, N'Kỹ năng giao tiếp.', N'Kỹ năng giao tiếp tốt, giọng nói dễ nghe.', 26, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1011, N'Kỹ năng văn phòng', N'Sử dụng thành thạo tin học văn phòng.', 20, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1012, N'Học vấn', N'Tốt nghiệp đại học chuyên ngành kế toán, kiểm toán.', 20, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1013, N'Phần mềm', N'Thành thạo Excel, phần mềm kế toán.', 20, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1014, N'Lập trình ', N'Ưu tiên biết lập trình cơ bản (PHP, Python).', 27, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1015, N'Chuyên môn server.', N' Hiểu biết về hệ thống mạng, server.', 27, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1016, N'Thiết kế ', N'Sử dụng thành thạo phần mền PhotoShop và các phần mềm thiết kế liên quan.', 29, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1017, N'Kỹ năng văn phòng', N'Sử dụng thành thạo tin học văn phòng.', 21, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1018, N'Chuyên môn kế toán', N'Tốt nghiệp đại học chuyên ngành kế toán, kiểm toán.', 21, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1019, N'Phần mềm', N'Thành thạo Excel, phần mềm kế toán.', 21, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1020, N'Lập trình ', N'Ưu tiên biết lập trình cơ bản (PHP, Python).', 24, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1021, N'Chuyên môn server.', N' Hiểu biết về hệ thống mạng, server.', 24, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1022, N'Kinh nghiệm.', N'Ưu tiên ứng viên có kinh nghiệm.', 22, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1023, N'Kỹ năng giao tiếp.', N'Kỹ năng giao tiếp tốt, giọng nói dễ nghe.', 22, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1024, N'Lập trình ', N'Ưu tiên biết lập trình cơ bản (PHP, Python).', 28, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1025, N'Chuyên môn server.', N' Hiểu biết về hệ thống mạng, server.', 28, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1026, N'Kinh nghiệm.', N'Ưu tiên ứng viên có kinh nghiệm.', 30, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1027, N'Kỹ năng giao tiếp.', N'Kỹ năng giao tiếp tốt, giọng nói dễ nghe.', 30, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1029, N'Leader', N'70%', NULL, 35, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1030, N'TeamWork', N'80%', NULL, 35, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1031, N'WebDesign', N'85%', NULL, 35, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1032, N'Backend Api', N'75%', NULL, 35, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1033, N'Marketing', N'tik tok , fb', 38, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1034, N'Marketing', N'tik tok , fb', 39, NULL, NULL)
GO
INSERT [dbo].[Skill] ([SkillId], [Name], [Description], [JobId], [CandidateId], [RecruiterId]) VALUES (1035, N'Sales', N'dsfasd', 39, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Skill] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240917204010_Update', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240918053026_UpdateCodeFirst', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240918090112_fix4', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240929091718_updateregister', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240929141504_addotp', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240930094208_addgender', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241003145200_update_candidate', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241008075013_updateresume', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241008092730_updatelinkskill', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241009185710_init', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241010150249_savejob', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241014084426_fixtable', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241015073221_fixCv', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241017120313_fixcandidatecv', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241020160748_fixcandidatecv2', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241118142610_Initial', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241120085452_template', N'8.0.10')
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 
GO
INSERT [dbo].[Admins] ([Id], [Name], [Email], [Password]) VALUES (1, N'duy', N'lahuuduy28@gmail.com', N'123')
GO
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 
GO
INSERT [dbo].[Contact] ([ID], [Name], [Email], [Telephone], [Subject], [Message]) VALUES (1, N'1', N'lahuuduy1234@gmail.com', N'1', N'1', N'1')
GO
INSERT [dbo].[Contact] ([ID], [Name], [Email], [Telephone], [Subject], [Message]) VALUES (2, N'1', N'hhungzspo2003@gmail.com', N'1', N'1', N'1')
GO
INSERT [dbo].[Contact] ([ID], [Name], [Email], [Telephone], [Subject], [Message]) VALUES (3, N'1', N'hhungzspo2003@gmail.com', N'1', N'1', N'1')
GO
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO

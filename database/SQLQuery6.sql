/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[Address]
      ,[City]
      ,[Zip]
      ,[Contact_no]
      ,[Email_id]
      ,[Password]
      ,[Last_login]
      ,[Active]
  FROM [OnlineAppointment].[dbo].[Patients]
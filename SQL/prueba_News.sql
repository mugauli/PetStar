/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *  FROM [DB_9F97CF_petstar].[dbo].[cmsNoticias] order by Fecha desc
SELECT *  FROM [DB_9F97CF_petstar].[dbo].[cmsLogError] order by 1 desc

  --truncate table [DB_9F97CF_petstar].[dbo].[cmsLogError]
  --truncate table [DB_9F97CF_petstar].[dbo].[cmsNoticias]

  --update cmsNoticias set Imagen = '' where id = 2
  --umb://media/aeab854235e542bcbf966cd9c4a20d7f

  --2/13/2019 12:00:00 AM----prueba de información

  --SELECT top 6 *  FROM [DB_9F97CF_petstar].[dbo].[cmsNoticias] where IdParent = 1297 order by Fecha desc 

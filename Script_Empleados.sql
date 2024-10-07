USE [DBPRUEBAS]
GO

/****** Object:  StoredProcedure [dbo].[USP_REGISTAR]    Script Date: 04/10/2024 03:26:42 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Empleados](
	[NumeroEmp] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellidos] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[NumeroEmp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

create PROCEDURE [dbo].[sp_lista_empleados]
AS
BEGIN
	
	SELECT NumeroEmp,Nombre,Apellidos FROM Empleados
END

CREATE PROCEDURE [dbo].[sp_guardar_empleado]
(
	@Nombre VARCHAR(60),
	@Apellidos             VARCHAR(60)
)
AS 
BEGIN
	INSERT INTO Empleados(Nombre,Apellidos) 
	VALUES
	(
		@Nombre,
		@Apellidos
	)
END
GO

CREATE PROCEDURE [dbo].[sp_editar_empleado]
(
	@NumeroEmp int,
	@Nombre VARCHAR(60),
	@Apellidos             VARCHAR(60)
)
AS 
BEGIN
	UPDATE Empleados SET Nombre=@Nombre,Apellidos=@Apellidos WHERE NumeroEmp=@NumeroEmp 
END
GO



create proc sp_elimnar_empleado
(
	@NumeroEmp int
)
as
begin
	delete from empleados where NumeroEmp = @NumeroEmp
end
go
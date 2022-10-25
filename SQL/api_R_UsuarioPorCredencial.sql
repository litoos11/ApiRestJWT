IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[api_R_UsuarioPorCredencial]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[api_R_UsuarioPorCredencial]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Angel Ortíz Sarmiento aortiz@moz.com.mz
-- Create date: 25/10/2022
-- Description:	Se crea procedimiento para obtener usuarios por credencial mediante una Api 
-- =============================================
CREATE PROCEDURE [dbo].[api_R_UsuarioPorCredencial]	
	@Login			VARCHAR(MAX)	= NULL
	,@Contrasenia	VARCHAR(MAX)	= NULL
AS BEGIN
	SET NOCOUNT ON;
	SELECT
			 U.LOCT_LOGIN_USUARIO	AS [LOGIN]
			,U.LOCT_LOGIN_CONTRA	AS [CONTRASENIA]
			,U.LOCT_LOGIN_NOMBRE	AS [NOMBRE]
			,U.LOCT_LOGIN_EMAIL		AS [EMAIL]
		FROM SECSEAAV2.dbo.LOCT_LOGIN_V U
		WHERE U.LOCT_LOGIN_USUARIO = @Login
		AND U.LOCT_LOGIN_CONTRA = @Contrasenia
END
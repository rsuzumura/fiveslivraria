alter proc spAltera_usuario(
@idUsuario	int	
,@nmUsuario	varchar(200)
,@dsEndereco	varchar(200)
,@tpUsuario	smallint
,@dsLogin	varchar(100)
,@senha	varchar(100)
,@dsPergunta	varchar(500)
,@dsResposta	int	
,@dsEmail	varchar(100)
)

as
begin

	update Usuarios set 
		nmUsuario =	@nmUsuario
		,dsEndereco =	@dsEndereco
		,tpUsuario =	@tpUsuario
		,dsLogin	= @dsLogin
		,senha	= @senha
		,dsPergunta	= @dsPergunta
		,dsResposta	= @dsResposta
		,dsEmail	= @dsEmail
	where idUsuario = @idUsuario

end
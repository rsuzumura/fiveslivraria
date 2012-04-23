alter proc spRemove_usuario(
@idUsuario int
)

as 
begin

delete from Usuarios
where idUsuario = @idUsuario

end
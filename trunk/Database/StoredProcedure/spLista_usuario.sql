declare @o varchar(100); set @o = 'spLista_usuario';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spLista_usuario (
	@nmUsuario varchar(100) = null,
	@roleName varchar(100) = null,
	@pageindex int,
	@pageSize int,
	@totalRowCount int out
) as
begin

	select
		@totalRowCount = count(*)
	from
		Usuarios
		inner join fiv_Logins on lgi_Username = dsLogin
		inner join fiv_Logins_Roles on lro_Username = dsLogin
	where
		(@nmUsuario is null or nmUsuario like @nmUsuario) and
		(@roleName is null or lro_Rolename = @roleName);

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;
	
	with [Usuario] as (
		select
			idUsuario,
			nmUsuario,
			dsEndereco,
			dsLogin,
			lgi_Email [Email],
			lgi_IsLockedOut [IsLocked],
			lro_Rolename [RoleName],
			row_number() over(order by nmUsuario) [rownum]
		from
			Usuarios
			inner join fiv_Logins on lgi_Username = dsLogin
			inner join fiv_Logins_Roles on lro_Username = dsLogin
		where
			(@nmUsuario is null or nmUsuario like @nmUsuario) and
			(@roleName is null or lro_Rolename = @roleName) and
			(lro_Rolename in('usuario','gestor'))
	)
	select
		*
	from
		[Usuario]
	where
		rownum between @startRow and @endRow
	for xml auto, elements, root('ListaUsuarios')
	
end

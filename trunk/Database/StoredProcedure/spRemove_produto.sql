declare @o varchar(100); set @o = 'spRemove_produto';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spRemove_produto (
	@idProduto int
) as
begin
	delete from 
		Produtos
	where
		idProduto = @idProduto;
end
go

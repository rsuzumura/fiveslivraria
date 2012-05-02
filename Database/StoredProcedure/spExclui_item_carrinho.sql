declare @o varchar(100); set @o = 'spExclui_item_carrinho';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spExclui_item_carrinho (
	@idCarrinho int
) as
begin
	
	
	delete from Carrinho where idCarrinho = @idCarrinho
	
	
end

<%@taglib uri="http://www.springframework.org/tags/form" prefix="forms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Cadastro de usu�rios</title>
</head>
<body>
	<h1>Cadastro de usu�rio</h1>
		
		<form action="cadastraUsuario" method="POST">
		<table>
			<tr>
				<td>Nome:</td>
				<td><input name="nmUsuario" type="text"></td>
			</tr>
			<tr>
				<td>Endere�o:</td>
				<td><input id="dsEndereco" type="text"></td>
			</tr>
			<tr>
				<td>Usu�rio:</td>
				<td><input id="dsLogin" type="text"></td>
			</tr>
			<tr>
				<td>Senha:</td>
				<td><input id="senha" type="text"></td>
			</tr>
			<tr>
				<td>Pergunta secreta:</td>
				<td><input id="dsPergunta" type="text"></td>
			</tr>
			<tr>
				<td>Resposta secreta:</td>
				<td><input id="dsResposta" type="text"></td>
			</tr>
			<tr>
				<td>E-mail:</td>
				<td><input id="dsEmail" type="text"></td>
			</tr>
			<tr>
				<td>Usuario</td>
				<td><input id="nmUsuario" type="text"></td>
			</tr>
		</table>
		<input id="btnCadastrar" type="submit" value="Cadastar">
	</form>
</body>
</html>
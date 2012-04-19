package projeto.livraria.classes;

import javax.validation.constraints.NotNull;


public class Usuario {
	
	public int getIdUsuario() {
		return idUsuario;
	}
	public void setIdUsuario(int idUsuario) {
		this.idUsuario = idUsuario;
	}
	public String getNmUsuario() {
		return nmUsuario;
	}
	public void setNmUsuario(String nmUsuario) {
		this.nmUsuario = nmUsuario;
	}
	public String getDsEndereco() {
		return dsEndereco;
	}
	public void setDsEndereco(String dsEndereco) {
		this.dsEndereco = dsEndereco;
	}
	public int getTpUsuario() {
		return tpUsuario;
	}
	public void setTpUsuario(int tpUsuario) {
		this.tpUsuario = tpUsuario;
	}
	public String getDsLogin() {
		return dsLogin;
	}
	public void setDsLogin(String dsLogin) {
		this.dsLogin = dsLogin;
	}
	public String getSenha() {
		return senha;
	}
	public void setSenha(String senha) {
		this.senha = senha;
	}
	public String getDsPergunta() {
		return dsPergunta;
	}
	public void setDsPergunta(String dsPergunta) {
		this.dsPergunta = dsPergunta;
	}
	public int getDsResposta() {
		return dsResposta;
	}
	public void setDsResposta(int dsResposta) {
		this.dsResposta = dsResposta;
	}
	public String getDsEmail() {
		return dsEmail;
	}
	public void setDsEmail(String dsEmail) {
		this.dsEmail = dsEmail;
	}
	public int idUsuario;
	@NotNull
	public String nmUsuario;
	@NotNull
	public String dsEndereco;
	@NotNull
	public int tpUsuario;
	@NotNull
	public String dsLogin;
	@NotNull
	public String senha;
	@NotNull
	public String dsPergunta;
	@NotNull
	public int dsResposta;
	@NotNull
	public String dsEmail;
	
	
}

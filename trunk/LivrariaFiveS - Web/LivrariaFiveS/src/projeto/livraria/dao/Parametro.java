package projeto.livraria.dao;

import java.util.ArrayList;
import java.util.List;

public class Parametro {

	public String nome;
	public Object valor;
	private List<Parametro> parametros;
	
	public Parametro(){
		parametros = new ArrayList<Parametro>();
	}
	
	private Parametro(String nome, Object valor)
	{
		this.nome = nome;
		this.valor = valor;				
	}
	
	public void add(String nome, Object valor)
	{
		parametros.add(new Parametro(nome, valor));		
	}
	
	public List<Parametro> get()
	{
		return parametros;
	}
}

package projeto.livraria.dao;

import java.math.BigDecimal;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.Date;
import java.sql.ResultSet;
import java.util.List;

public class CallStoredProcedure {

	private Connection con;
	
	public CallStoredProcedure()
	{
		con = new ConnectionFactory().getConnection();		
	}
	
	public ResultSet executaSp(String sp, List<Parametro> parametros)
	{
		ResultSet rs = null;
		//A proc deve vir neste formato "{call dbo.nomeProc(?, ?)}"
		 try {
		      CallableStatement cstmt = con.prepareCall(sp);
		      
		      for (Parametro parametro : parametros) {
					switch (parametro.valor.getClass().getSimpleName()) {
					case "String":
						cstmt.setString(parametro.nome, (String)parametro.valor);
						break;
					case "Integer":
						cstmt.setInt(parametro.nome, (Integer)parametro.valor);
						break;
					case "Float":
						cstmt.setFloat(parametro.nome, (Float)parametro.valor);
						break;
					case "BigDecimal":
						cstmt.setBigDecimal(parametro.nome, (BigDecimal) parametro.valor);
						break;
					case "Date":
						cstmt.setDate(parametro.nome, (Date)parametro.valor);
						break;
					default:
						break;
					}
				}	
		      //Caso necessario parametros de output um exemplo
		      //cstmt.registerOutParameter("managerID", java.sql.Types.INTEGER);
		      //System.out.println("MANAGER ID: " + cstmt.getInt("managerID"));
		      rs = cstmt.executeQuery();
		      
		      cstmt.close();
		   }
		   catch (Exception e) {
		      e.printStackTrace();
		   }
		 return rs;			
	}	
}
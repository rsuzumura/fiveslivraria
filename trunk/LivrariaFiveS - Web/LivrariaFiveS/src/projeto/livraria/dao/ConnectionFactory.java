package projeto.livraria.dao;

import java.sql.Connection;
import java.sql.DriverManager;

public class ConnectionFactory {

	private final String url = "jdbc:microsoft:sqlserver://";
	private final String serverName= "localhost";
	private final String portNumber = "1433";
	private final String databaseName= "pubs";
	private final String userName = "user";
	private final String password = "password";
	// Informs the driver to use server a side-cursor, 
	// which permits more than one active statement 
	// on a connection.
	private final String selectMethod = "cursor"; 

	public Connection getConnection() {		
		try {
			Class.forName("com.microsoft.jdbc.sqlserver.SQLServerDriver"); 
			return DriverManager.getConnection(getConnectionUrl(), userName, password);
		} catch(Exception e) {
			throw new RuntimeException(e);
		}
	}	 

	private String getConnectionUrl(){
		return url+serverName+":"+portNumber+";databaseName="+databaseName+";selectMethod="+selectMethod+";";
	}

}

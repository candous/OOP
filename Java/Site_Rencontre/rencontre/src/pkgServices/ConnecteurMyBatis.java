package pkgServices;

import java.io.IOException;
import java.io.InputStream;

import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

public class ConnecteurMyBatis {

	private static SqlSessionFactory sqlSessionFactory;

	public static SqlSessionFactory getSqlSessionFactory() {
		
		if (sqlSessionFactory==null) {
			
			InputStream inputStream;
			
			try {
				inputStream=Resources.getResourceAsStream("pkgServices/mybatis-config.xml");
				sqlSessionFactory=new SqlSessionFactoryBuilder().build(inputStream);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		return sqlSessionFactory;
	}
	
	public static SqlSession ouvrirSession(){
		
		return getSqlSessionFactory().openSession();
	}
}

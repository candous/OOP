package Actions;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class ActionLogOut {

	public static void logOut(HttpServletRequest request, HttpServletResponse response)
	{
	
		request.getSession().invalidate();
		
		try {
			
			request.getRequestDispatcher("servletIndex").forward(request, response);
		
		} catch (ServletException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
}

package pkgListeners;

import java.io.IOException;

import javax.faces.context.ExternalContext;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpSession;
import javax.servlet.http.HttpSessionEvent;
import javax.servlet.http.HttpSessionListener;

import pkgEntities.Membre;
import pkgManagers.MembreManager;


public class SessionListeners implements HttpSessionListener {

	@Override
	public void sessionCreated(HttpSessionEvent arg0) {
		// TODO Auto-generated method stub

	}

	@Override
	public void sessionDestroyed(HttpSessionEvent event) {
		// TODO Auto-generated method stub
		HttpSession session=event.getSession();
		Membre user=(Membre)session.getAttribute("user");
		
		//mettre a jour status du user dans la BD
		int offline=MembreManager.updateOnline(user.getMembreId(), false);
	}

}

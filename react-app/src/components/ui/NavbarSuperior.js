import React from 'react';

const NavbarSuperior = () => {
    return (
        <nav className="navbar navbar-expand navbar-light navbar-bg">
				<a className="sidebar-toggle">
          <i className="hamburger align-self-center"></i>
        </a>			

			<div className="navbar-collapse collapse">
					<ul className="navbar-nav navbar-align">
						<li className="nav-item dropdown">
							<a className="nav-icon dropdown-toggle d-inline-block d-sm-none" href="#" data-bs-toggle="dropdown">
                <i className="align-middle" data-feather="settings"></i>
              </a>

							<a className="nav-link dropdown-toggle d-none d-sm-inline-block" href="#" data-bs-toggle="dropdown">
                            <img src={"./assets/dist/img/avatars/avatar-2.jpg"} className="avatar img-fluid rounded-circle me-1" alt="Chris Wood" /> <span className="text-dark">Angel Pinilla</span>
              </a>
							<div className="dropdown-menu dropdown-menu-end">
								<a className="dropdown-item" href="#"><i className="align-middle me-1" data-feather="user"></i> Profile</a>								
								<div className="dropdown-divider"></div>																
								<a className="dropdown-item" href="#">Sign out</a>
							</div>
						</li>
					</ul>
				</div>
			</nav>
    );
};

export default NavbarSuperior;

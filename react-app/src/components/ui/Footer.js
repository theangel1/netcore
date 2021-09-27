import React from 'react';

const Footer = () => {
    return (
        <footer className="footer">
        <div className="container-fluid">
            <div className="row text-muted">
                <div className="col-6 text-start">
                    <ul className="list-inline">
                        <li className="list-inline-item">
                            <a className="text-muted" href="#">Support</a>
                        </li>
                        <li className="list-inline-item">
                            <a className="text-muted" href="#">Contrato</a>
                        </li>
                        <li className="list-inline-item">
                            <a className="text-muted" href="#">Nosotros</a>
                        </li>
                   
                    </ul>
                </div>
                <div className="col-6 text-end">
                    <p className="mb-0">
                        &copy; 2021 - <a href="index.html" className="text-muted">Wallmart App</a>
                    </p>
                </div>
            </div>
        </div>
    </footer>

    );
};

export default Footer;
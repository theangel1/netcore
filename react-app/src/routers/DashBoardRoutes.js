import React from "react";
import { Redirect, Route, Switch } from "react-router";
import CargosScreen from "../components/cargos/CargosScreen";
import HomeScreen from "../components/home/HomeScreen";
import Footer from "../components/ui/Footer";
import { Navbar } from "../components/ui/Navbar";
import NavbarSuperior from "../components/ui/NavbarSuperior";

const DashBoardRoutes = () => {
  return (
    <>
      <Navbar />

      <div className="main">
        <NavbarSuperior />
        <main class="content">
                  <div class="container-fluid p-0">
                      <div className="row">
            <Switch>
              <Route exact path="/cargos" component={CargosScreen} />
              <Route exact path="/hero/:heroeId" component={HomeScreen} />
              <Route exact path="/dc" component={HomeScreen} />
              <Route exact path="/search" component={HomeScreen} />

              <Redirect to="/" component={HomeScreen} />
            </Switch>
            </div>
          </div>
        </main>

        <Footer />
      </div>
    </>
  );
};

export default DashBoardRoutes;

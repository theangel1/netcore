import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import LoginScreen from '../components/login/LoginScreen';
import DashBoardRoutes from './DashBoardRoutes';

const AppRouter = () => {
    
  return (
    <Router>      
        <Switch>
          <Route exact path="/login" component={LoginScreen} />
          <Route path="/" component={DashBoardRoutes} />
        </Switch>      
    </Router>
  );
}
export default AppRouter;
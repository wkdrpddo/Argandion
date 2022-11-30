import React from "react";
import ReactDOM from "react-dom";
import { createBrowserHistory } from "history";
import {
    Route,
    Switch,
    HashRouter
} from "react-router-dom";

import './assets/scss/style.scss';
import 'bootstrap/dist/css/bootstrap.min.css';

import MainPage from "./views/mainPage/mainIndex.jsx";
import downloadPage from "./views/subPage/download.jsx";
import introductionPage from "./views/subPage/introduction.jsx";
import guidePage from "./views/subPage/guide.jsx";
import copyrightPage from "./views/subPage/copyright.jsx";

var hist = createBrowserHistory();

ReactDOM.render(
    <HashRouter history={hist}>
        <Switch>
            <Route path="/download" component={downloadPage} />
            <Route path="/introduction" component={introductionPage} />
            <Route path="/guide" component={guidePage} />
            <Route path="/copyright" component={copyrightPage} />
            <Route component={MainPage} />
        </Switch>
    </HashRouter>,
    document.getElementById("root")
);

import React from "react";
import { Link, NavLink } from "react-router-dom";

export const Navbar = () => {
  return (
    <nav id="sidebar" className="sidebar">
      <div className="sidebar-content js-simplebar">
        <a className="sidebar-brand" href="index.html">
          <svg
            version="1.1"
            x="0px"
            y="0px"
            width="20px"
            height="20px"
            viewBox="0 0 20 20"
            enableBackground="new 0 0 20 20"
          >
            <path
              d="M19.4,4.1l-9-4C10.1,0,9.9,0,9.6,0.1l-9,4C0.2,4.2,0,4.6,0,5s0.2,0.8,0.6,0.9l9,4C9.7,10,9.9,10,10,10s0.3,0,0.4-0.1l9-4
              C19.8,5.8,20,5.4,20,5S19.8,4.2,19.4,4.1z"
            />
            <path
              d="M10,15c-0.1,0-0.3,0-0.4-0.1l-9-4c-0.5-0.2-0.7-0.8-0.5-1.3c0.2-0.5,0.8-0.7,1.3-0.5l8.6,3.8l8.6-3.8c0.5-0.2,1.1,0,1.3,0.5
              c0.2,0.5,0,1.1-0.5,1.3l-9,4C10.3,15,10.1,15,10,15z"
            />
            <path
              d="M10,20c-0.1,0-0.3,0-0.4-0.1l-9-4c-0.5-0.2-0.7-0.8-0.5-1.3c0.2-0.5,0.8-0.7,1.3-0.5l8.6,3.8l8.6-3.8c0.5-0.2,1.1,0,1.3,0.5
              c0.2,0.5,0,1.1-0.5,1.3l-9,4C10.3,20,10.1,20,10,20z"
            />
          </svg>

          <span className="align-middle me-3">Wallmart App</span>
        </a>

        <ul className="sidebar-nav">
          <li className="sidebar-header">Pages</li>

          <li className="sidebar-item">
            <a
              data-bs-target="#pages"
              data-bs-toggle="collapse"
              className="sidebar-link collapsed"
            >
              <i className="align-middle" data-feather="layout"></i>{" "}
              <span className="align-middle">Pages</span>
            </a>
            <ul
              id="pages"
              className="sidebar-dropdown list-unstyled collapse "
              data-bs-parent="#sidebar"
            >
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-profile.html">
                  Profile
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-settings.html">
                  Settings
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-clients.html">
                  Clients
                </a>
              </li>
              <li className="sidebar-item">
                <a
                  data-bs-target="#projects"
                  data-bs-toggle="collapse"
                  className="sidebar-link collapsed"
                >
                  Projects
                </a>
                <ul
                  id="projects"
                  className="sidebar-dropdown list-unstyled collapse "
                >
                  <li className="sidebar-item">
                    <a className="sidebar-link" href="pages-projects-list.html">
                      List
                    </a>
                  </li>
                  <li className="sidebar-item">
                    <a
                      className="sidebar-link"
                      href="pages-projects-detail.html"
                    >
                      Detail{" "}
                      <span className="badge badge-sidebar-primary">New</span>
                    </a>
                  </li>
                </ul>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-invoice.html">
                  Invoice
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-pricing.html">
                  Pricing
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-tasks.html">
                  Tasks
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-chat.html">
                  Chat <span className="badge badge-sidebar-primary">New</span>
                </a>
              </li>
              <li className="sidebar-item">
                <a className="sidebar-link" href="pages-blank.html">
                  Blank Page
                </a>
              </li>
            </ul>
          </li>
        </ul>
      </div>
      </nav>      
  );
};

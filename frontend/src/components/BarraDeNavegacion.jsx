import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";

function BarraDeNavegacion() {
  return (
    <Navbar expand="lg" className="navbar px-5 py-2 bg-success">
        <img src="./public/logo.png" alt="Logo de la empresa" className="logo me-3 bg-success"/>
        <Navbar.Brand href="#home" className="text-light titulo bg-success fs-2 navbar-Brand">
          Centro Basura Cero
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
    </Navbar>
  );
}

export default BarraDeNavegacion;

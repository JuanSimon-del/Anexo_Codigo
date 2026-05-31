import { Container, Row, Col } from "react-bootstrap";

function Footer() {
  return (
    <footer className="bg-success text-light py-4 border-top border-secondary footer-fixed-width">
      <Container>
        <Row>
          <Col md={4} className="mb-4">
            <div className="mx-auto footer-left-content ms-2">
              <h5 className="mb-3">Trabajo Final Integrador</h5>
              <p className="mb-1">Materia: Simulación</p>
              <p className="mb-1">Comisión: 4K1</p>
              <p className="mb-1">4° Año Ingeniería en Sistemas</p>
              <p className="mb-0">Universidad Tecnológica Nacional — Facultad Regional Tucumán</p>
            </div>
          </Col>

          <Col md={5} className="mb-4 mb-md-0 d-flex flex-column">
            <div className="w-50 mx-auto">
              <h5 className="mb-3">Estudiantes</h5>
              <p className="mb-1">Cabrera Navarro, Tomás</p>
              <p className="mb-1">Ceballos, María Emilia</p>
              <p className="mb-1">Darelli, Damián Agustín</p>
              <p className="mb-1">Jiménez, Corina Abigail</p>
              <p className="mb-1">Ruiz, Luciana</p>
              <p className="mb-0">Simón, Juan Enrique</p>
            </div>
          </Col>

          <Col md={3} className="mb-4 mb-md-0 d-flex flex-column">
          <div className="mx-auto">
            <h5 className="mb-3">Profesores</h5>
            <p className="mb-1">Paredi, Mario Luis</p>
            <p className="mb-0">Teri, María Eugenia</p>
          </div>
          <img src="../public/utn-frt.png" alt="logo UTN" className="w-50 m-auto"/>
          </Col>
        </Row>
      </Container>
    </footer>
  );
}

export default Footer;

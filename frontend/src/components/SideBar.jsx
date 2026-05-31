import { Button, Navbar } from "react-bootstrap";
import Container from "react-bootstrap/Container";
import { Row, Col } from "react-bootstrap";

function SideBar({ campanas = 1, trabajadores = [2, 2, 2, 2], selectedCampaign = 1, onSelectCampaign = () => {} }) {
  const etapas = [
    { icono: "bi bi-eye", nombre: "Inspección Visual", color: "text-info" },
    { icono: "bi-tools", nombre: "Desarme y Testeo", color: "text-secondary"},
    { icono: "bi-arrows-expand-vertical", nombre: "Separación de Materiales", color: "text-warning" },
    { icono: "bi-recycle", nombre: "Reciclaje", color: "text-success" },
  ];

  return (
    <>
      <Navbar className="bg-dark w-25 min-vh-100 align-items-start sidebar-fixed py-4 px-3">
        <section className="w-100 p-4">
          <h3>Configuración Inicial</h3>
          <article className="my-4">
            <h5>Parámetros de la Simulación</h5>
            <Row className="d-flex my-4">
              <Col lg={8}>
                <p className="my-auto">Cantidad de Campañas: </p>
              </Col>
              <Col>
                <input type="text" value={campanas} className="ms-3 w-50 text-center" readOnly />
              </Col>
            </Row>
          </article>
          <article className="my-4">
            <h5>Distribución de Trabajadores</h5>
            {etapas.map((etapa, index) => (
              <Row key={index} className="d-flex align-items-center my-4">
                <Col>
                  <i className={`bi ${etapa.icono} ${etapa.color} ms-3`}></i>
                </Col>
                <Col lg={8}>
                  <p className="mb-0 ms-2">{etapa.nombre}</p>
                </Col>
                <Col>
                  <input type="text" className="me-2 w-100 text-center" value={trabajadores[index]} readOnly />
                </Col>
              </Row>
            ))}
          </article>
          <article>
            <h5 className="my-4">Resultados por Campaña ({campanas})</h5>
            {Array.from({ length: campanas }, (_, i) => {
              const number = i + 1;
              const active = selectedCampaign === number;
              return (
                <Button
                  key={i}
                  className={`w-100 mb-2 ${active ? "btn-primary" : "btn-secondary"}`}
                  onClick={() => onSelectCampaign(number)}
                >
                  Campaña {number}
                </Button>
              );
            })}
          </article>

          <div className="sidebar-truck mt-3 d-flex align-items-center">
            <img src="../public/camion.png" alt="Camión" className="truck-img" />
          </div>
        </section>
      </Navbar>
    </>
  );
}

export default SideBar;

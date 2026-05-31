import { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import "../../src/styles.css";
import { Row, Col, Card } from "react-bootstrap";

function ResumenGeneral() {
  const [datos, setDatos] = useState(null);

  useEffect(() => {
    const resultados = localStorage.getItem("simulationResults");
    if (resultados) {
      const parsed = JSON.parse(resultados);
      const summary = parsed.Summary || parsed.summary || parsed;
      setDatos({
        totalDevicesArrived: Number(summary.totalDevicesArrived ?? summary.TotalDevicesArrived ?? 0),
        refurbishedCount: Number(summary.refurbishedCount ?? summary.RefurbishedCount ?? 0),
        recycledCount: Number(summary.recycledCount ?? summary.RecycledCount ?? 0),
        cantidadPlacasRecuperadas: Number(summary.cantidadPlacasRecuperadas ?? summary.CantidadPlacasRecuperadas ?? 0),
        hierroRecuperadoKg: Number(summary.hierroRecuperadoKg ?? summary.HierroRecuperadoKg ?? 0),
        aluminioRecuperadoKg: Number(summary.aluminioRecuperadoKg ?? summary.AluminioRecuperadoKg ?? 0),
        cobreRecuperadoKg: Number(summary.cobreRecuperadoKg ?? summary.CobreRecuperadoKg ?? 0),
        plasticoAltaCalidad: Number(summary.plasticoAltaCalidad ?? summary.PlasticoAltaCalidad ?? 0),
        plasticoMediaCalidad: Number(summary.plasticoMediaCalidad ?? summary.PlasticoMediaCalidad ?? 0),
        plasticoBajaCalidad: Number(summary.plasticoBajaCalidad ?? summary.PlasticoBajaCalidad ?? 0),
      });
    }
  }, []);

  if (!datos) {
    return <div className="px-5 my-3 text-light">Cargando datos...</div>;
  }

  return (
    <>
      <section className="px-5 my-3">
        <h3>Resumen General (Todas las Campañas)</h3>
        <Row className="mt-1 g-4">
          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="cards">
                <i className="bi bi-laptop text-info fs-1"></i>
                <p className="fs-2">{datos.totalDevicesArrived}</p>
                <h5>Equipos ingresados</h5>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="cards">
                <i className="bi bi-tools text-secondary fs-1"></i>
                <p className="fs-2">{datos.refurbishedCount}</p>
                <h5>Equipos refuncionalizados</h5>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="cards">
                <i className="bi bi-recycle text-success fs-1"></i>
                <p className="fs-2">{datos.recycledCount}</p>
                <h5>Equipos reciclados</h5>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="cards">
                <i className="bi bi-cpu text-warning fs-1"></i>
                <p className="fs-2">{datos.cantidadPlacasRecuperadas}</p>
                <h5>Placas Recuperadas</h5>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </section>

      <section className="px-5 d-flex flex-column">
        <Row className="mt-5">
          <h3>Materiales Recuperados (Total Acumulado)</h3>
        </Row>

        <Row className="mt-1 g-4">
          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="d-flex flex-column align-items-center cards">
                <h5 className="mt-3">Hierro</h5>
                <div className="contorno d-flex align-items-center rounded-2 my-4">
                  <div className="relleno mx-auto d-flex align-items-center justify-content-center fs-5">
                    Fe
                  </div>
                </div>
                <p className="fs-5">{datos.hierroRecuperadoKg.toFixed(2)} kg</p>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="d-flex flex-column align-items-center cards">
                <h5 className="mt-3">Aluminio</h5>
                <div className="contorno d-flex align-items-center rounded-2 my-4">
                  <div className="relleno mx-auto d-flex align-items-center justify-content-center fs-5">
                    Al
                  </div>
                </div>
                <p className="fs-5">{datos.aluminioRecuperadoKg.toFixed(2)} kg</p>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="d-flex flex-column align-items-center cards">
                <h5 className="mt-3">Cobre</h5>
                <div className="contorno d-flex align-items-center rounded-2 my-4">
                  <div className="relleno mx-auto d-flex align-items-center justify-content-center fs-5">
                    Cu
                  </div>
                </div>
                <p className="fs-5">{datos.cobreRecuperadoKg.toFixed(2)} kg</p>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="d-flex flex-column align-items-center cards">
                <h5 className="mt-3">Plástico</h5>
                <i
                  className="bi bi-cup-straw vasito-plastico"
                  style={{ fontSize: "70px" }}
                ></i>
                <p className="fs-5 mt-3">{(datos.plasticoAltaCalidad + datos.plasticoMediaCalidad + datos.plasticoBajaCalidad).toFixed(2)} kg</p>
              </Card.Body>
            </Card>
          </Col>

          <Col>
            <Card className="text-center h-100 border-success border-2 shadow-sm">
              <Card.Body className="d-flex flex-column align-items-center cards">
               <h5 className="mt-3">Placas</h5>
                <i
                  className="bi bi-motherboard my-1"
                  style={{ fontSize: "70px" }}
                ></i>
                <p className="fs-5 mt-3">{datos.cantidadPlacasRecuperadas}</p>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </section>
    </>
  );
}

export default ResumenGeneral;

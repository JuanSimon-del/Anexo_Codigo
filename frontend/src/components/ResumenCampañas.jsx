import { useState, useEffect } from "react";
import { Col, Row } from "react-bootstrap";
import Table from "react-bootstrap/Table";

function ResumenCampañas({ selectedCampaign = 1 }) {
  const [campanas, setCampanas] = useState([]);

  useEffect(() => {
    const resultados = localStorage.getItem("simulationResults");
    if (resultados) {
      const parsed = JSON.parse(resultados);
      setCampanas(parsed.campaignDetails || []);
    }
  }, []);

  if (!campanas || campanas.length === 0) {
    return <div className="mx-5 text-light my-5">No hay datos de campañas</div>;
  }

  const selectedIndex = Math.max(0, Math.min(selectedCampaign - 1, campanas.length - 1));
  const campana = campanas[selectedIndex];

  return (
    <>
      <section className="mx-5">
        <h3 className="mt-5">Campaña {selectedIndex + 1}</h3>
          <Row>
            <Col md={6}>
              <h5 className="my-4">Ingreso de dispositivos</h5>
              <Table striped bordered hover variant="dark">
                <tbody>
                  <tr>
                    <th className="w-75">Dispositivos Ingresados</th>
                    <td className="text-center">{campana.totalDevicesArrived}</td>
                  </tr>
                  <tr>
                    <th className="w-75">Laptops</th>
                    <td className="text-center">{campana.laptopsArrived}</td>
                  </tr>
                  <tr>
                    <th className="w-75">Desktops</th>
                    <td className="text-center">{campana.desktopsArrived}</td>
                  </tr>
                </tbody>
              </Table>
            </Col>
            <Col md={6}>
              <h5 className="my-4">Resultado del Procesamiento</h5>
              <Table striped bordered hover variant="dark">
                <tbody>
                  <tr>
                    <th className="w-75">Equipos Refuncionalizados</th>
                    <td className="w-50 text-center">{campana.refurbishedCount}</td>
                  </tr>
                  <tr>
                    <th className="w-75">Equipos Reciclados</th>
                    <td className="text-center">{campana.recycledCount}</td>
                  </tr>
                  <tr>
                    <th className="w-75">Placas Recuperadas</th>
                    <td className="text-center">{campana.cantidadPlacasRecuperadas}</td>
                  </tr>
                </tbody>
              </Table>
            </Col>
          </Row>
          
          <Row>
            <Col>
              <h5 className="my-4">Estadísticas de Etapas</h5>
              <Table striped bordered hover variant="dark">
                <thead>
                  <tr>
                    <th>Etapa</th>
                    <th>Procesados</th>
                    <th>Cola Máxima</th>
                  </tr>
                </thead>
                <tbody>
                  {campana.stages && campana.stages.map((stage, stageIndex) => (
                    <tr key={stageIndex}>
                      <td>{stage.stageName}</td>
                      <td className="text-center">{stage.processedCount}</td>
                      <td className="text-center">{stage.maxQueueSize}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </Col>
          </Row>

          <Row>
            <Col>
              <h5 className="my-4">Materiales Recuperados</h5>
              <Table striped bordered hover variant="dark">
                <tbody>
                  <tr>
                    <th className="w-75">Aluminio</th>
                    <td className="text-center">{campana.aluminioRecuperadoKg.toFixed(2)} kg</td>
                  </tr>
                  <tr>
                    <th className="w-75">Cobre</th>
                    <td className="text-center">{campana.cobreRecuperadoKg.toFixed(2)} kg</td>
                  </tr>
                  <tr>
                    <th className="w-75">Hierro</th>
                    <td className="text-center">{campana.hierroRecuperadoKg.toFixed(2)} kg</td>
                  </tr>
                  <tr>
                    <th className="w-75">Plástico Alta Calidad</th>
                    <td className="text-center">{campana.plasticoAltaCalidad.toFixed(2)} kg</td>
                  </tr>
                  <tr>
                    <th className="w-75">Plástico Media Calidad</th>
                    <td className="text-center">{campana.plasticoMediaCalidad.toFixed(2)} kg</td>
                  </tr>
                  <tr>
                    <th className="w-75">Plástico Baja Calidad</th>
                    <td className="text-center">{campana.plasticoBajaCalidad.toFixed(2)} kg</td>
                  </tr>
                </tbody>
              </Table>
            </Col>
          </Row>
        </section>
    </>
  );
}

export default ResumenCampañas;

import { useState } from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import MiDropdown from "./MiDropdown";
import DropdownTrabajadores from "./DropdownTrabajadores";
import { useNavigate } from "react-router-dom";
import { runSimulation } from "../services/simulationService";

function Simulacion() {
  const navigate = useNavigate();
  
  const [campanas, setCampanas] = useState(1);
  const [trabajadores, setTrabajadores] = useState([2, 2, 2, 2]); // [etapa1, etapa2, etapa3, etapa4]
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState(null);

  const handleActualizarTrabajador = (etapa, valor) => {
    const nuevosTrabajadores = [...trabajadores];
    nuevosTrabajadores[etapa] = valor;
    setTrabajadores(nuevosTrabajadores);
  };

  const handleSimular = async () => {
    setCargando(true);
    setError(null);
    try {
      const resultados = await runSimulation(campanas, trabajadores);
      // Guardar resultados en localStorage para acceder desde el informe
      localStorage.setItem("simulationResults", JSON.stringify(resultados));
      // Guardar configuración
      localStorage.setItem("simulationConfig", JSON.stringify({ campanas, trabajadores }));
      navigate("/informe");
    } catch (err) {
      setError("Error al ejecutar la simulación: " + err.message);
      console.error(err);
    } finally {
      setCargando(false);
    }
  };

  return (
    <Container fluid className="py-3">
      <h3 className="my-4 ms-5 text-light">
        Número de Trabajadores por Proceso
      </h3>
      <Row className="gx-3 gy-4">
        <Col xs={12} md={6} lg={3} className="d-flex flex-column align-items-center">
          <DropdownTrabajadores 
            etapa={0} 
            valor={trabajadores[0]} 
            onChange={(val) => handleActualizarTrabajador(0, val)}
          />
          <div>
            <img
              src="../public/etapa1.png"
              alt="Etapa 1"
              className="etapa1 p-2"
            />
          </div>
          <div className="mt-auto">
            <img
              src="../public/engranaje.png"
              className="engranaje"
              alt="Engranaje para visualizar el procesamiento en cada etapa"
            />
          </div>
        </Col>
        <Col xs={12} md={6} lg={3} className="d-flex flex-column align-items-center">
          <DropdownTrabajadores 
            etapa={1} 
            valor={trabajadores[1]} 
            onChange={(val) => handleActualizarTrabajador(1, val)}
          />
          <div>
            <img
              src="../public/etapa2.png"
              alt="Etapa 2"
              className="etapa2 p-4"
            />
          </div>
          <div className="mt-auto">
            <img
              src="../public/engranaje.png"
              className="engranaje mx-auto"
              alt="Engranaje para visualizar el procesamiento en cada etapa"
            />
          </div>
        </Col>
        <Col xs={12} md={6} lg={3} className="d-flex flex-column align-items-center">
          <DropdownTrabajadores 
            etapa={2} 
            valor={trabajadores[2]} 
            onChange={(val) => handleActualizarTrabajador(2, val)}
          />
          <div>
            <img
              src="../public/etapa3.png"
              alt="Etapa 3"
              className="etapa3 p-3"
            />
          </div>
          <div className="mt-auto">
            <img
              src="../public/engranaje.png"
              className="engranaje mx-auto"
              alt="Engranaje para visualizar el procesamiento en cada etapa"
            />
          </div>
        </Col>
        <Col xs={12} md={6} lg={3} className="d-flex flex-column align-items-center">
          <DropdownTrabajadores 
            etapa={3} 
            valor={trabajadores[3]} 
            onChange={(val) => handleActualizarTrabajador(3, val)}
          />
          <div>
            <img src="../public/etapa4.png" alt="Etapa 4" className="etapa4" />
          </div>
          <div className="mt-auto">
            <img
              src="../public/engranaje.png"
              className="engranaje mx-auto"
              alt="Engranaje para visualizar el procesamiento en cada etapa"
            />
          </div>
        </Col>
      </Row>
      
      {error && (
        <div className="alert alert-danger mt-3 w-100" role="alert">
          {error}
        </div>
      )}
      
      <section className="d-flex w-100 my-5 justify-content-center">
        <input
          type="number"
          placeholder="Campañas a simular"
          value={campanas}
          onChange={(e) => {
            const value = Number(e.target.value);
            setCampanas(Number.isNaN(value) ? 1 : Math.max(1, Math.min(50, value)));
          }}
          className="ms-5 px-3 border border-1 rounded-2 input bg-light me-2"
          min="1"
          max="50"
        />
        <button 
          className="btn btn-success ms-3 px-5"
          onClick={handleSimular}
          disabled={cargando}
        >
          {cargando ? "Simulando..." : "Simular"}
        </button>
      </section>
    </Container>
  );
}

export default Simulacion;

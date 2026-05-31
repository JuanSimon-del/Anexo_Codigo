import { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import "./styles.css";
import BarraDeNavegacion from "./components/BarraDeNavegacion";
import Simulacion from "./components/Simulacion";
import Informe from "./components/Informe";
import { Col, Row } from "react-bootstrap";
import Footer from "./components/Footer";
import { BrowserRouter, Routes, Route, useLocation } from "react-router-dom";

function App() {
  const [count, setCount] = useState(0);

  function Content() {
    const location = useLocation();
    const hasSidebar = location.pathname.startsWith("/informe");

    return (
      <div className={`app-content ${hasSidebar ? "with-sidebar" : ""}`}>
        <Routes>
          <Route path="/" element={<Simulacion />} />
          <Route path="/informe" element={<Informe />} />
        </Routes>
        <Footer />
      </div>
    );
  }

  return (
    <BrowserRouter>
      <BarraDeNavegacion />
      <Content />
    </BrowserRouter>
  );
}

export default App;

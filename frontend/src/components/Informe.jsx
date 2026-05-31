import { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import SideBar from "./SideBar";
import ResumenGeneral from "./ResumenGeneral";
import ResumenCampañas from "./ResumenCampañas";

function Informe() {
  const [campanas, setCampanas] = useState(1);
  const [trabajadores, setTrabajadores] = useState([2, 2, 2, 2]);
  const [selectedCampaign, setSelectedCampaign] = useState(1);

  useEffect(() => {
    const resultados = localStorage.getItem("simulationResults");
    const config = localStorage.getItem("simulationConfig");
    if (config) {
      const { campanas: c, trabajadores: t } = JSON.parse(config);
      setCampanas(c);
      setTrabajadores(t);
      setSelectedCampaign(1);
    }
  }, []);

  return (
    <>
      <div className="d-flex informe-container">
        <SideBar
          campanas={campanas}
          trabajadores={trabajadores}
          selectedCampaign={selectedCampaign}
          onSelectCampaign={setSelectedCampaign}
        ></SideBar>
        <div className="flex-grow-1 d-flex flex-column informe-content">
          <main className="flex-grow-1 p-4 informe-main">
            <ResumenGeneral></ResumenGeneral>
            <ResumenCampañas selectedCampaign={selectedCampaign}></ResumenCampañas>
          </main>
        </div>
      </div>
    </>
  );
}

export default Informe;

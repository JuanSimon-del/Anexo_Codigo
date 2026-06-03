// Servicio para llamar a la API del backend
const BASE_URL = import.meta.env.VITE_API_URL || "";
const API_URL = `${BASE_URL}api/simulation`;

export const runSimulation = async (totalCampaigns, workersPerStage) => {
  try {
    const response = await fetch(`${API_URL}/run`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        totalCampaigns: parseInt(totalCampaigns),
        workersPerStage: workersPerStage.map(w => parseInt(w)),
      }),
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error al ejecutar la simulación:", error);
    throw error;
  }
};

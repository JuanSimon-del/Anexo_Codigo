import Dropdown from "react-bootstrap/Dropdown";

function DropdownTrabajadores({ etapa, valor, onChange }) {
  return (
    <>
      <section className="d-flex w-100 my-3 align-items-center justify-content-center">
        {/* Input que actúa como texto de guía fija + muestra el valor actual */}
        <input
          type="text"
          readOnly
          value={`Trabajadores Etapa ${etapa}: ${valor}`}
          className="ms-5 px-3 border border-1 rounded-2 input bg-light me-2 text-center"
          style={{ width: "220px", fontWeight: "500" }}
        />
        
        {/* Desplegable para seleccionar la cantidad */}
        <Dropdown onSelect={(e) => onChange(Number(e))}>
          <Dropdown.Toggle
            variant="secondary"
            id={`dropdown-${etapa}`}
            className="px-3"
          >
            {/* Dejamos el toggle limpio o con el valor según prefieras, variant secondary le da el gris de la foto */}
          </Dropdown.Toggle>

          <Dropdown.Menu>
            {[1, 2, 3, 4, 5].map((num) => (
              <Dropdown.Item 
                key={num} 
                eventKey={num}
                active={valor === num}
              >
                {num}
              </Dropdown.Item>
            ))}
          </Dropdown.Menu>
        </Dropdown>
      </section>
    </>
  );
}

export default DropdownTrabajadores;
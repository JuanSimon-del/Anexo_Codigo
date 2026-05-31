import Dropdown from "react-bootstrap/Dropdown";

function DropdownTrabajadores({ etapa, valor, onChange }) {
  return (
    <>
      <section className="d-flex w-100 my-3">
        <input
          type="number"
          placeholder="Ej: 5 trabajadores"
          value={valor}
          onChange={(e) => onChange(parseInt(e.target.value) || 0)}
          className="ms-5 px-3 border border-1 rounded-2 input bg-light me-2"
          min="1"
        />
        <Dropdown>
          <Dropdown.Toggle variant="success" id={`dropdown-${etapa}`}>
            {valor || "Seleccionar"}
          </Dropdown.Toggle>
          <Dropdown.Menu>
            {[1, 2, 3, 4, 5].map((num) => (
              <Dropdown.Item
                key={num}
                onClick={() => onChange(num)}
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

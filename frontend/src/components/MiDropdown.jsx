import Dropdown from "react-bootstrap/Dropdown";

function MiDropdown() {
  return (
    <>
        <Dropdown>
          <Dropdown.Toggle variant="success"></Dropdown.Toggle>

          <Dropdown.Menu>
            <Dropdown.Item href="#/action-1">1 campaña</Dropdown.Item>
            <Dropdown.Item href="#/action-2">2 campañas</Dropdown.Item>
            <Dropdown.Item href="#/action-3">3 campañas</Dropdown.Item>
          </Dropdown.Menu>
        </Dropdown>
    </>
  );
}

export default MiDropdown;

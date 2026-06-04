# Simulador de Planta de Tratamiento de E-Waste

Este proyecto fue desarrollado como parte del **Trabajo Final Integrador (TFI)**, con el objetivo de modelar matemática y lógicamente los procesos de recepción, inspección, desarme y clasificación de computadoras (Laptops y Desktops).

## Características Principales

* **Motor de Eventos Discretos Personalizado:** Implementado desde cero en C#, gestiona una línea de tiempo virtual simulando llegadas, inicios, transición y duración entre campañas.
* **Sistema de Ruteo Dinámico:** Las entidades se transforman durante la simulación. Una computadora entera puede convertirse en un equipo reacondicionado o ser desarmado, transformándose en múltiples entidades de "Materiales" (Hierro, Aluminio, Cobre y Plásticos).
* **Detección de Cuellos de Botella:** El sistema monitoriza en tiempo real el tamaño de las colas de espera en cada etapa de la planta, permitiendo analizar la eficiencia en la asignación de personal.
* **Precisión Matemática:** Utiliza generadores de números pseudoaleatorios para aplicar distribuciones de probabilidad en los tiempos de llegada, tiempos de servicio y peso de los dispositivos.

## Arquitectura y Tecnologías

* **Lenguajes:** C# (.NET) y Javascript (REACT).
* **Arquitectura:** Orientada a Objetos (POO) y diseño en capas (Core, Domain, Controllers).
* **Tipo de Aplicación:** API RESTful.

### Flujo de las Etapas Simuladas
El simulador modela 4 etapas secuenciales de trabajo:
1. **Inspección:** Revisión inicial de los equipos que llegan en lotes semanales.
2. **Desarme y Testeo:** Separación de componentes estructurales y extracción de la placa madre.
3. **Separación:** Tratamiento físico/químico para separar metales (Hierro, Aluminio, Cobre) y plásticos.
4. **Clasificación:** Subdivisión de los plásticos recuperados según su pureza (Alta, Media, Baja calidad).

## Instalación

1. Clona este repositorio:
   ```bash
   git clone https://github.com/JuanSimon-del/Anexo_Codigo

2. Ubicarse, mediante consola, en el directorio ./backend/SimulacionTFI 

3. Ejecutar el compilador de C# utilizando el siguiente codigo
   ```bash
   dotnet run --SimulacionTFI

4. Abrir el navegador web con el URL brindado por Microsoft Hosting Lifetime. Por defecto, es http://localhost:5000

# Simulación - 2026
## Grupo 5 - 4K1 - Integrantes
* 50428 - Cabrera, Tomas Navarro
* 56442 - Ceballos, Maria Emilia
* 52781 - Darelli, Damián Agustin
* 53281 - Jimenez, Corina Abigail
* 56601 - Ruiz, Luciana
* 52543 - Simon, Juan Enrique


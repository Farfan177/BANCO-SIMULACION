# Simulador Básico de Banco en Consola
**Asignatura:** Estructuras de Datos  
**Lenguaje:** C# | **Tipo:** Consola

---

## Estructura del Proyecto

```
BancoSimulador/
├── BancoSimulador.csproj
├── Program.cs
│
├── Entidades/
│   ├── Cliente.cs          → Datos del cliente (ID, nombre, cuenta, saldo)
│   └── Transaccion.cs      → Registro de depósitos/retiros con tipo y saldo anterior
│
├── Estructuras/
│   ├── NodoCliente.cs          → Nodo de la lista enlazada
│   ├── ListaEnlazadaClientes.cs → Lista enlazada manual (clientes)
│   ├── NodoCola.cs             → Nodo de la cola
│   ├── ColaAtencion.cs         → Cola FIFO manual (turnos)
│   ├── NodoPila.cs             → Nodo de la pila
│   └── PilaTransacciones.cs    → Pila LIFO manual (historial/reversión)
│
├── Logica/
│   └── Banco.cs            → Lógica central del sistema bancario
│
└── UI/
    └── Menu.cs             → Menú e interacción con el usuario en consola
```

---

## Cómo ejecutar

### Requisitos
- .NET SDK 8.0 o superior
- Visual Studio 2022 / Visual Studio Code / Rider

### Pasos
1. Clonar el repositorio
2. Abrir la solución en Visual Studio
3. Ejecutar con `F5` o desde terminal:
```bash
cd BancoSimulador
dotnet run
```

---

## Estructuras de datos utilizadas

| Estructura | Clase | Uso en el sistema |
|---|---|---|
| **Lista enlazada** | `ListaEnlazadaClientes` | Almacenar y gestionar todos los clientes |
| **Cola (FIFO)** | `ColaAtencion` | Turnos de atención (primero en llegar, primero atendido) |
| **Pila (LIFO)** | `PilaTransacciones` | Historial y reversión de la última transacción |

> **Nota:** Ninguna de estas estructuras usa `Queue`, `Stack`, `LinkedList`, `List<T>` ni colecciones nativas de .NET. Todas están implementadas manualmente con nodos y referencias.

---

## Menú del sistema

```
1.  Registrar cliente
2.  Listar clientes
3.  Buscar cliente
4.  Agregar cliente a la cola de atención
5.  Atender siguiente cliente
6.  Realizar depósito
7.  Realizar retiro
8.  Consultar saldo
9.  Deshacer última transacción
10. Mostrar cola de atención
11. Mostrar total de clientes
12. Mostrar total de dinero del banco
13. Salir
```

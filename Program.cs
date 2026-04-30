using BancoSimulador.Logica;
using BancoSimulador.UI;

// ─────────────────────────────────────────────────────────────
//  SIMULADOR BÁSICO DE BANCO - ESTRUCTURAS DE DATOS
//  Lenguaje: C#  |  Tipo: Consola
//  Estructuras utilizadas:
//    - Lista enlazada → Gestión de clientes
//    - Cola (FIFO)    → Atención por turnos
//    - Pila (LIFO)    → Historial y reversión de transacciones
// ─────────────────────────────────────────────────────────────

Banco banco = new Banco("BANCO DATOS S.A.");
Menu menu = new Menu(banco);
menu.Ejecutar();

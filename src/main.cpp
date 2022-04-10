//                                  ┌∩┐(◣_◢)┌∩┐                                \\
//                                                                              \\
// main.cpp (09/04/2022)                                                        \\
// Autor: Antonio Mateo (.\Moon Antonio)  -  NIRVANA                            \\
//******************************************************************************\\

/* Programa de inicio de nirvana */

#include <iostream>
#ifdef WIN32
#include <windows.h>
#include "nucleo/ventana.h"
#endif

#ifdef WIN32
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow){
    Ventana ventana("Prueba", hInstance, nCmdShow);
    ventana.abrirVentana();
    return 0;
}
#else
int main(){
    std::cout << "Hola" << std::endl;
    return 0;
}
#endif
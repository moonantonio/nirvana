//                                  ┌∩┐(◣_◢)┌∩┐                                \\
//                                                                              \\
// ventana.cpp (09/04/2022)                                                     \\
// Autor: Antonio Mateo (.\Moon Antonio)  -  NIRVANA                            \\
//******************************************************************************\\

#include "ventana.h"

Ventana::Ventana(const char * ventana, HINSTANCE hInstance, int nComando) {
    nombre = ventana;
    comando = nComando;

    // Crear clase
    WNDCLASS wc = { };
    wc.lpfnWndProc = Ventana::WindowProc;
    wc.hInstance = hInstance;
    wc.lpszClassName = this->nombre;
    RegisterClass(&wc);

    // Crear Ventana
    hwnd = CreateWindowEx(
            0,                      // Estilo opcional de ventana
            this->nombre,           // Clase de ventana
            this->nombre,           // Texto de ventana
            WS_POPUP | WS_VISIBLE | WS_SYSMENU,    // Estilo de ventana
            CW_USEDEFAULT,          // Posicion X
            CW_USEDEFAULT,          // Posicion Y
            CW_USEDEFAULT,          // Ancho de ventana
            CW_USEDEFAULT,          // Alto de ventana
            NULL,                   // Ventana superior
            NULL,                   // Menu
            hInstance,              // Instancia
            NULL                    //Datos adicionales de la aplicacion
            );
}

LRESULT Ventana::WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
        case WM_DESTROY:
            PostQuitMessage(0);
            return 0;
        case WM_PAINT:{
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hwnd, &ps);
            FillRect(hdc, &ps.rcPaint, (HBRUSH)(COLOR_WINDOW + 1));
            EndPaint(hwnd, &ps);
        }
        return 0;
    }
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}

int Ventana::abrirVentana() {
    if (hwnd == NULL) {
        return 0;
    }
    ShowWindow(hwnd, comando);
    MSG msg = { };
    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    return 0;
}

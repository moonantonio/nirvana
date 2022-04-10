//                                  ┌∩┐(◣_◢)┌∩┐                                \\
//                                                                              \\
// ventana.h (09/04/2022)                                                       \\
// Autor: Antonio Mateo (.\Moon Antonio)  -  NIRVANA                            \\
//******************************************************************************\\

#ifdef WIN32
#include <windows.h>
#endif

class Ventana{
    private:
        const char* nombre;
        HWND hwnd;
        int comando;
    public: static LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
    public:
        Ventana(const char *, HINSTANCE, int);
        int abrirVentana();
};

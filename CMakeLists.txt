cmake_minimum_required(VERSION 3.20)
project(nirvana VERSION      0.0.1
        DESCRIPTION "Motor CPP"
        HOMEPAGE_URL https://moonantonio.github.io/
        LANGUAGES CXX)
set(CMAKE_CXX_STANDARD 14)
set(CODIGO src/main.cpp)
set(MODULOS src/nucleo/ventana.cpp src/nucleo/ventana.h)
add_executable(WIN32 ${CODIGO} ${MODULOS})


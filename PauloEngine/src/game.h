#pragma once

#include <SDL.h>
#include <string>

#define TICKRATE (1000 / 60)

void InitializeWindow(int width, int height, std::string title);
void DestroyWindow();

bool WindowShouldClose();

void Tick();
void Render();

void Run();
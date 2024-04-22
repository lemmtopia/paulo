#include "platform.h"
#include <iostream>

SDL_Window* window;
SDL_Renderer* renderer;
bool isRunning = false;

int lastTicks;

void InitializeWindow(int width, int height, std::string title)
{
	if (SDL_Init(SDL_INIT_EVERYTHING) != 0)
	{
		std::cout << "Error initializing SDL\n";
		return;
	}

	window = SDL_CreateWindow(title.c_str(), SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, width, height, NULL);

	if (!window)
	{
		std::cout << "Error creating the window\n";
		return;
	}

	renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);

	if (!renderer)
	{
		std::cout << "Error creating the renderer\n";
		return;
	}

	isRunning = true;
}

void DestroyWindow()
{
	SDL_DestroyRenderer(renderer);
	SDL_DestroyWindow(window);
	SDL_Quit();
}

bool WindowShouldClose()
{
	SDL_Event ev;
	SDL_PollEvent(&ev);

	return ev.type == SDL_QUIT;
}

void Tick()
{
	int ticks = TICKRATE - (SDL_GetTicks() - lastTicks);
	lastTicks = SDL_GetTicks();

	if (ticks > 0 && ticks < TICKRATE)
	{
		SDL_Delay(ticks);
	}
}

void Render()
{
	SDL_RenderClear(renderer);
	SDL_RenderPresent(renderer);
}
#include "game.h"
#include <iostream>

#include "ECS/ECS.h"

SDL_Window* window;
SDL_Renderer* renderer;
bool isRunning = false;

int lastTicks;

ECS_t ecs;

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
	SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
	SDL_RenderClear(renderer);
	SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

	SystemDraw(ecs, renderer);

	SDL_RenderPresent(renderer);
}

void Run()
{
	ecs = InitECS(3);

	while (isRunning)
	{
		if (WindowShouldClose())
		{
			isRunning = false;
		}

		Tick();
		Render();
	}

	DestroyECS(ecs);
}
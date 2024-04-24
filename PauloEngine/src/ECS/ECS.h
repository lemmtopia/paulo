#pragma once

#include <SDL.h>

/* ECS Components */
typedef struct
{
	double x;
	double y;

	double scaleX;
	double scaleY;

	double rotation;
} TransformComponent_t;

typedef struct
{
	int w;
	int h;
	SDL_Texture* texture;
} SpriteComponent_t;

/* ECS main functions */
typedef struct
{
	int len;
	int* entities;
	TransformComponent_t* transforms;
	SpriteComponent_t* sprites;
} ECS_t;

ECS_t InitECS(int n);
void DestroyECS(ECS_t &ecs);

void SystemDraw(ECS_t ecs, SDL_Renderer* renderer);
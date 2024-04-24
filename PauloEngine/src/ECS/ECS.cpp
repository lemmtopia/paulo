#include "ECS.h"

#include <stdlib.h>

ECS_t InitECS(int n)
{
	ECS_t ecs;
	ecs.len = n;

	ecs.entities = (int*)malloc(n * sizeof(int));
	ecs.transforms = (TransformComponent_t*)malloc(n * sizeof(TransformComponent_t));
	ecs.sprites = (SpriteComponent_t*)malloc(n * sizeof(SpriteComponent_t));

	for (int i = 0; i < n; i++)
	{
		ecs.entities[i] = i;
		ecs.transforms[i] = { (double)i * 3, (double)i * 10, 0, 0, 0 };
		ecs.sprites[i] = { NULL };
	}

	return ecs;
}

void DestroyECS(ECS_t& ecs)
{
	free(ecs.entities);
	free(ecs.transforms);
	free(ecs.sprites);
}

void SystemDraw(ECS_t ecs, SDL_Renderer* renderer)
{
	for (int i = 0; i < ecs.len; i++)
	{
		SDL_Rect rect = {
			(int)ecs.transforms[i].x,
			(int)ecs.transforms[i].y,
			16, 16
		};

		SDL_RenderFillRect(renderer, &rect);
	}
}
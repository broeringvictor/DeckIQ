﻿using System.Runtime.Intrinsics.X86;
using DeckIQ.Api.Data;
using DeckIQ.Core.Common.Extensions;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace DeckIQ.Api.Handlers;

public class FlashCardHandler(AppDbContext context) : IFlashCardHandler
{
    public async Task<Response<FlashCard?>> CreateAsync(CreateFlashCardRequest request)
    {
        try
        {
            var flashCard = new FlashCard
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreateDate = DateTime.Now,
                Question = request.Question,
                Answer = request.Answer,
                IncorrectAnswerA = request.IncorrectAnswerA,
                IncorrectAnswerB = request.IncorrectAnswerB,
                IncorrectAnswerC = request.IncorrectAnswerC,
                IncorrectAnswerD = request.IncorrectAnswerD,
                CardImage = request.CardImage,
            };
            await context.FlashCards.AddAsync(flashCard);
            await context.SaveChangesAsync();
            return new Response<FlashCard?>(flashCard, 201, "Flash Card criada.");

        }
        catch (Exception e)
        {
            return new Response<FlashCard?>(null, 500, "Não foi possível criar o Flash Card.");
        }
    }

    public async Task<Response<FlashCard?>> DeleteAsync(DeleteFlashCardRequest request)
    {
        {
            try
            {
                var flashCard = await context
                    .FlashCards
                    .FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);
                
                

                
                if (flashCard is null)
                    return new Response<FlashCard?>(null, 404, "Transação não encontrada");

                context.FlashCards.Remove(flashCard);
                await context.SaveChangesAsync();
                
                return new Response<FlashCard?>(flashCard, 201, "Flash Card criada.");
                
            }
            catch (Exception e)
            {
                return new Response<FlashCard?>(null, 500, "Não foi possível deletar o Flash Card.");
            }
        }
    }

    public async Task<Response<FlashCard?>> UpdateAsync(UpdateFlashCardRequest request)
    {
        try
        {
            var flashCard = await context.FlashCards.FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);

            if (flashCard is null)
                return new Response<FlashCard?>(null, 404, "Transação não encontrada");

            flashCard.CategoryId = request.CategoryId;
            flashCard.LastUpdateDate = DateTime.Now;
            flashCard.Question = request.Question;
            flashCard.Answer = request.Answer;
            flashCard.IncorrectAnswerA = request.IncorrectAnswerA;
            flashCard.IncorrectAnswerB = request.IncorrectAnswerB;
            flashCard.IncorrectAnswerC = request.IncorrectAnswerC;
            flashCard.IncorrectAnswerD = request.IncorrectAnswerD;
            flashCard.CardImage = request.CardImage;
            
            context.FlashCards.Update(flashCard);
            await context.SaveChangesAsync();

            return new Response<FlashCard?>(flashCard);


        }
        catch (Exception e)
        {
            return new Response<FlashCard?>(null, 500, "Não foi possível editar o Flash Card.");
        }
    }

    public async Task<Response<FlashCard?>> GetByIdAsync(GetFlashCardByIdRequest request)
    {
        try
        {
            var flashCard = await context.FlashCards
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return flashCard is null
                ? new Response<FlashCard?>(null, 404, "FlashCard não encontrado")
                : new Response<FlashCard?>(flashCard);
        }
        catch
        {
            return new Response<FlashCard?>(null, 500, "Não foi possível recuperar o FlashCard");
        }
    }

    public async Task<Response<List<FlashCard?>?>> GetByPeriod(GetFlashCardSByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFristDay();
            request.EndDate = DateTime.Now.GetLastDay();
        }
        catch (Exception e)
        {
            return new PagedResponse<List<FlashCard?>?>(null, 500, "Não foi possível determinar a data de inicio ou final.");
        }

        try
        {


            var query =  context.FlashCards.AsNoTracking().Where(
                    x => x.LastUpdateDate
                         >= request.StartDate
                         && x.LastUpdateDate <= request.EndDate
                         && x.UserId == request.UserId)
                .OrderByDescending(x => x.CreateDate);
           
            var flashCards = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
        
            return new PagedResponse<List<FlashCard?>?>(
                flashCards!,
                count,
                request.PageNumber,
                request.PageSize);
            
        }
        catch
        {
            return new PagedResponse<List<FlashCard?>?>(null, 500, "Não foi possível obter os Flash Cards.");   
        }
    }
}
   
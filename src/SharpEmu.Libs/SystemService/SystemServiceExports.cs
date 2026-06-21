// Copyright (C) 2026 SharpEmu Emulator Project
// SPDX-License-Identifier: GPL-2.0-or-later

using SharpEmu.HLE;
using System.Buffers.Binary;

namespace SharpEmu.Libs.SystemService;

public static class SystemServiceExports
{
    private const int OrbisSystemServiceErrorParameter = unchecked((int)0x80A10003);
    private const int SystemServiceStatusSize = 0x0C;
    private const int DisplaySafeAreaInfoSize = sizeof(float) + 128;

    [SysAbiExport(
        Nid = "rPo6tV8D9bM",
        ExportName = "sceSystemServiceGetStatus",
        Target = Generation.Gen4 | Generation.Gen5,
        LibraryName = "libSceSystemService")]
    public static int SystemServiceGetStatus(CpuContext ctx)
    {
        var statusAddress = ctx[CpuRegister.Rdi];
        if (statusAddress == 0)
        {
            return SetReturn(ctx, OrbisSystemServiceErrorParameter);
        }

        Span<byte> status = stackalloc byte[SystemServiceStatusSize];
        status.Clear();
        BinaryPrimitives.WriteInt32LittleEndian(status, 0);
        status[0x06] = 1;

        return ctx.Memory.TryWrite(statusAddress, status)
            ? SetReturn(ctx, 0)
            : SetReturn(ctx, (int)OrbisGen2Result.ORBIS_GEN2_ERROR_MEMORY_FAULT);
    }

    [SysAbiExport(
        Nid = "1n37q1Bvc5Y",
        ExportName = "sceSystemServiceGetDisplaySafeAreaInfo",
        Target = Generation.Gen4 | Generation.Gen5,
        LibraryName = "libSceSystemService")]
    public static int SystemServiceGetDisplaySafeAreaInfo(CpuContext ctx)
    {
        var infoAddress = ctx[CpuRegister.Rdi];
        if (infoAddress == 0)
        {
            return SetReturn(ctx, OrbisSystemServiceErrorParameter);
        }

        Span<byte> info = stackalloc byte[DisplaySafeAreaInfoSize];
        info.Clear();
        BinaryPrimitives.WriteSingleLittleEndian(info, 1.0f);

        return ctx.Memory.TryWrite(infoAddress, info)
            ? SetReturn(ctx, 0)
            : SetReturn(ctx, (int)OrbisGen2Result.ORBIS_GEN2_ERROR_MEMORY_FAULT);
    }

    private static int SetReturn(CpuContext ctx, int result)
    {
        ctx[CpuRegister.Rax] = unchecked((ulong)result);
        return result;
    }
}
